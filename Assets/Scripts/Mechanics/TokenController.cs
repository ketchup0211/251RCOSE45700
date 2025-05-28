using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class animates all token instances in a scene.
    /// This allows a single update call to animate hundreds of sprite 
    /// animations.
    /// If the tokens property is empty, it will automatically find and load 
    /// all token instances in the scene at runtime.
    /// </summary>
    public class TokenController : MonoBehaviour
    {
        [Tooltip("Frames per second at which tokens are animated.")]
        public float frameRate = 12;

        [Tooltip("Instances of tokens which are animated. If empty, token instances are found and loaded at runtime.")]
        public TokenInstance[] tokens;

        float nextFrameTime = 0;

        [ContextMenu("Find All Tokens")]
        void FindAllTokensInScene()
        {
            tokens = UnityEngine.Object.FindObjectsByType<TokenInstance>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
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
                for (var i = 0; i < tokens.Length; i++)
                {
                    var token = tokens[i];

                    if (token != null && token._renderer != null && token.sprites != null && token.sprites.Length > 0)
                    {
                        if (token.frame >= token.sprites.Length)
                            token.frame = 0;

                        token._renderer.sprite = token.sprites[token.frame];

                        if (token.collected && token.frame == token.sprites.Length - 1)
                        {
                            token.gameObject.SetActive(false);
                            tokens[i] = null;
                        }
                        else
                        {
                            token.frame = (token.frame + 1) % token.sprites.Length;
                        }
                    }
                }

                nextFrameTime += 1f / frameRate;
            }
        }
    }
}
