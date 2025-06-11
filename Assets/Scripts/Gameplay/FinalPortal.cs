using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class FinalPortal : MonoBehaviour
    {
        public string nextSceneName = "Stage1";

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
