using UnityEngine;

namespace Agricosmic.Utilities
{
    /// <summary>
    /// Hovers an object in any direction
    /// </summary>
    public class HoverAnimation : MonoBehaviour
    {
        [Tooltip("The frequency to hover at")]
        [SerializeField] private float _frequency = 0.5f;
        [Tooltip("The amount to move up and down by")]
        [SerializeField] private float _amplitude = 0.125f;
        [Tooltip("The axis to hover along")]
        [SerializeField] private Vector3 _axis = Vector3.up;
        [Tooltip("Should we start at a random point along our range of travel?")]
        [SerializeField] private bool _randomizeStart = true;
        
        private Vector3 _originalPosition = Vector3.zero;
        private float _randomInitial = 0f;

        private void Start()
        {
            _originalPosition = transform.localPosition;
            if (_randomizeStart)
                _randomInitial = Random.value * _frequency;
        }

        private void Update()
        {
            transform.localPosition = _originalPosition +
                                 _axis * (Mathf.Sin((Time.time + _randomInitial) * Mathf.PI * _frequency) * _amplitude);
        }
    }
}