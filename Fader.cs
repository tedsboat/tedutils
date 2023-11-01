using System;
using UnityEngine;

namespace Agricosmic.Utilities
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        private bool _inTempMode = false;
        
        private float _time;
        private float _maxTime;
        private Color _color;

        private void Start()
        {
            if (_renderer == null) _renderer = GetComponent<SpriteRenderer>();

            _renderer.color = new(1, 1, 1, 0);
        }

        private void Update()
        {
            if (_inTempMode)
            {
                if (_time > 0f)
                {
                    _time -= Time.deltaTime;

                    _renderer.color = new(_color.r, _color.g, _color.b, _time / _maxTime);
                    _renderer.enabled = true;
                }
                else
                {
                    _renderer.enabled = false;
                }
            }
            else
            {
                _renderer.color = _color;
            }
        }

        public void SetAlphaAndColor(Color color)
        {
            _color = color;
        }

        public void FadeOutForTime(Color color, float time)
        {
            _inTempMode = true;
            
            _renderer.color = color;
            _color = color;
            
            _maxTime = time;
            _time = time;
        }
    }
}