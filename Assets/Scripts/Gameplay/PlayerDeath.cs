using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="PlayerDeath"></typeparam>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            if (player.health.IsAlive)
            {
                player.health.Die();
                model.virtualCamera.Follow = null;
                model.virtualCamera.LookAt = null;
                // player.collider.enabled = false;
                player.controlEnabled = false;

                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);
                player.animator.SetTrigger("hurt");
                player.animator.SetTrigger("die");
                player.animator.SetBool("dead", true);
                /* TODO: 사망 시 이펙트 (정지 -> 잠깐 공중부양 -> 낙하) */
                //player.BeginDeathEffect(); 

                // UI Manager 호출
                Object.FindFirstObjectByType<UIManager>()?.AddDeath();

                Simulation.Schedule<PlayerSpawn>(1.3f);
            }
        }
    }
}