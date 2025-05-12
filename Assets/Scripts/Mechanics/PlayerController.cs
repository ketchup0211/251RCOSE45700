using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip[] respawnSounds;
        public AudioClip ouchAudio;

        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        public int maxJumpCount = 2;
        private int jumpCount = 0;

        // 최소 점프 보장용 변수
        float jumpPressTime = 0f;
        float minJumpHoldTime = 0.1f; // 최소 점프 유지 시간
        bool jumpPressed = false;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        public Collider2D collider2d;
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");

                // 점프 시작
                if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount < maxJumpCount)
                {
                    jumpState = JumpState.PrepareToJump;
                    jumpCount++;
                    jumpPressed = true;
                    jumpPressTime = 0f;
                }
                // 점프 버튼 뗐을 때
                else if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    jumpPressed = false;
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }

            // 점프 누르고 있는 시간 누적
            if (jumpPressed)
            {
                jumpPressTime += Time.deltaTime;
            }

            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                        jumpCount = 0;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;

                // 최소 유지 시간보다 짧게 누른 경우만 감쇠 무시
                if (jumpPressTime >= minJumpHoldTime && velocity.y > 0)
                {
                    velocity.y *= model.jumpDeceleration;
                }
            }

            // 좌우 반전
            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);

            // 미세 속도 흔들림 제거용 클램핑
            if (Mathf.Abs(move.x) < 0.01f)
                move.x = 0f;

            float vx = Mathf.Abs(velocity.x);
            if (vx < 0.01f)
                vx = 0f;

            animator.SetFloat("velocityX", vx);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}
