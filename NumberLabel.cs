using System.Collections.Generic;
using UnityEngine;

namespace Agricosmic.Utilities
{
    /// <summary>
    /// Represents a single-digit number from sprites with optional outlining
    /// </summary>
    public class NumberLabel : MonoBehaviour
    {
        [Tooltip("Sprites in order [0-9]")]
        [SerializeField] private List<Sprite> _numbers;
        [Tooltip("Sprites in order [0-9] outlined")]
        [SerializeField] private List<Sprite> _outlinedNumbers;
        [Tooltip("The value to show")]
        [SerializeField] private int _value;
        [Tooltip("Use the outlined sprites?")]
        [SerializeField] private bool _outline;
        [Tooltip("The color of the text")] 
        [SerializeField] private Color _color;
        [Tooltip("Reference to the renderer to change. Located on start-up if null")]
        [SerializeField] private SpriteRenderer _sprite;

        private void Awake()
        {
            if (_sprite == null) _sprite = GetComponent<SpriteRenderer>();
        }

        private void UpdateSprite()
        {
            _sprite.sprite = _outline ? _outlinedNumbers[_value] : _numbers[_value];
            _sprite.color = _color;
        }

        /// <summary>
        /// Quick shortcut to hiding the sprite
        /// </summary>
        public void HideLabel()
        {
            _sprite.gameObject.SetActive(false);
        }

        /// <summary>
        /// Quick shortcut to showing the sprite
        /// </summary>
        public void ShowLabel()
        {
            _sprite.gameObject.SetActive(true);
        }

        /// <summary>
        /// Sets the outline mode
        /// </summary>
        /// <param name="isOutlined">yay or nay</param>
        public void SetOutlined(bool isOutlined)
        {
            _outline = isOutlined;
        }
        
        /// <summary>
        /// Set the value to represent
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(int value)
        {
            _value = value;
            UpdateSprite();
        }

        public void SetColor(Color color)
        {
            _color = color;
            UpdateSprite();
        }

        public void SetOpacity(float opacity)
        {
            _color.a = opacity;
            UpdateSprite();
        }

        private void Update()
        {
            if (!Application.isPlaying)
                UpdateSprite();
        }
    }
}