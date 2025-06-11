using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class Stage1Portal : MonoBehaviour
    {
        public string nextSceneName = "Stage2";

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
