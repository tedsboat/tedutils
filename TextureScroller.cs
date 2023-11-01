using UnityEngine;

namespace Agricosmic.Utilities
{
    /// <summary>
    /// Updates the offset of a material to scroll a texture seamlessly over time.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class TextureScroller : MonoBehaviour
    {
        [Tooltip("Reference to the renderer to update. Located on start-up if null")]
        [SerializeField] private SpriteRenderer _renderer;
        [Tooltip("The speed to scroll in the X direction")]
        [SerializeField] private float _xSpeed = 0.1f;
        [Tooltip("The speed to scroll in the Y direction")]
        [SerializeField] private float _ySpeed;
        
        private static readonly int MAIN_TEX = Shader.PropertyToID("_MainTex");

        private void Start()
        {
            if (_renderer == null) _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _renderer.material.SetTextureOffset(MAIN_TEX, 
                new(_xSpeed * Time.time, _ySpeed * Time.time));
        }
    }
}