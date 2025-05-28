using UnityEngine;

namespace Platformer.Mechanics
{
    public class TokenController : MonoBehaviour
    {
        public float frameRate = 12;
        public TokenInstance[] tokens;
        float nextFrameTime = 0;

        [ContextMenu("Find All Tokens")]
        void FindAllTokensInScene()
        {
            tokens = UnityEngine.Object.FindObjectsByType<TokenInstance>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        }

        void Awake()
        {
            if (tokens.Length == 0)
                FindAllTokensInScene();

            for (var i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] != null)
                {
                    tokens[i].tokenIndex = i;
                    tokens[i].controller = this;
                }
            }
        }

        void Update()
        {
            if (Time.time - nextFrameTime > (1f / frameRate))
            {
                foreach (var token in tokens)
                {
                    if (token != null && token._renderer != null && token.sprites.Length > 0 && token.gameObject.activeSelf)
                    {
                        token._renderer.sprite = token.sprites[token.frame];
                        token.frame = (token.frame + 1) % token.sprites.Length;
                    }
                }
                nextFrameTime = Time.time;
            }
        }
    }
}
