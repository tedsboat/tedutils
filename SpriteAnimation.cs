using UnityEngine;
using UnityEngine.Events;

namespace Agricosmic.Utilities
{
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private Sprite[] _frames = { };
        [SerializeField] private float _timePerFrame = 0.2f;
        [SerializeField] private bool _loop = true;
        [SerializeField] private bool _autoplay = true;
        [SerializeField] private SpriteRenderer _renderer;

        public UnityEvent AnimationCycled;
        
        private bool _playing = false;
        private int _frameIndex = 0;
        private float _frameTime = 0f;

        private void Start()
        {
            if (_renderer == null) _renderer = GetComponent<SpriteRenderer>();
            if (_autoplay) _playing = true;
        }

        private void Update()
        {
            if (_frames.Length == 0)
            {
                Debug.LogWarning("SpriteAnimation on " + name + " has no frames! Pausing...");
                _playing = false;
            }

            if (_playing)
            {
                _frameTime += Time.deltaTime;

                if (_frameTime >= _timePerFrame)
                {
                    AdvanceFrame();
                }
            }

            UpdateGraphics();
        }

        public void Play() => _playing = true;

        public void Pause() => _playing = false;

        public void Reset()
        {
            _frameTime = 0;
            _frameIndex = 0;
        }

        public void Stop()
        {
            _playing = false;
            Reset();
        }

        public void AdvanceFrame()
        {
            _frameTime = 0;
            _frameIndex++;
            if (_frameIndex >= _frames.Length)
            {
                if (_loop)
                {
                    _frameIndex = 0;
                }
                else
                {
                    _frameIndex = _frames.Length - 1;
                    _playing = false;
                    AnimationCycled?.Invoke();
                }
            }
        }

        private void UpdateGraphics()
        {
            _renderer.sprite = _frames[_frameIndex];
        }
    }
}