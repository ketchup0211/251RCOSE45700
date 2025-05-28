using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class TokenInstance : MonoBehaviour
    {
        public AudioClip tokenCollectAudio;
        public bool randomAnimationStartTime = false;
        public Sprite[] idleAnimation, collectedAnimation;

        internal Sprite[] sprites = new Sprite[0];
        internal SpriteRenderer _renderer;

        internal int tokenIndex = -1;
        internal TokenController controller;
        internal int frame = 0;
        internal bool collected = false;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            sprites = idleAnimation;

            if (randomAnimationStartTime && sprites.Length > 0)
                frame = Random.Range(0, sprites.Length);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
                OnPlayerEnter(player);
        }

        void OnPlayerEnter(PlayerController player)
        {
            if (collected) return;

            collected = true;
            frame = 0;
            sprites = collectedAnimation;

            if (_renderer != null)
                _renderer.sprite = collectedAnimation.Length > 0 ? collectedAnimation[0] : null;

            if (tokenCollectAudio != null)
                AudioSource.PlayClipAtPoint(tokenCollectAudio, transform.position);

            Object.FindFirstObjectByType<UIManager>()?.AddToken();

            // 바로 비활성화
            gameObject.SetActive(false);

            var ev = Schedule<PlayerTokenCollision>();
            ev.token = this;
            ev.player = player;
        }

        public void ResetToken()
        {
            collected = false;
            sprites = idleAnimation;
            frame = 0;
            _renderer.enabled = true;
            gameObject.SetActive(true);
        }
    }
}
