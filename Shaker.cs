using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Agricosmic.Utilities
{
    public class Shaker : MonoBehaviour
    {
        [SerializeField] private float _intensity = 1f;

        private float _shakeTime = 0f;
        private Vector3 _center;

        private void Start()
        {
            _center = transform.position;
        }

        private void Update()
        {
            if (_shakeTime > 0f)
            {
                _shakeTime -= Time.unscaledDeltaTime;
                var offset = Random.insideUnitCircle * _shakeTime * _intensity;
                transform.position = (Vector2)_center + offset;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _center, 0.2f);
            }
        }

        public void Shake(float time)
        {
            _shakeTime = time;
        }

        public void StopShake()
        {
            _shakeTime = 0f;
        }
    }
}