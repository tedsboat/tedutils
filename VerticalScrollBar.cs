using UnityEngine;

namespace Agricosmic.Utilities
{
    /// <summary>
    /// DEPRECATED - Moves its parent based on its vertical position
    /// <br/>
    /// TODO: @xavier needs rework :)
    /// </summary>
    public class VerticalScrollBar : MonoBehaviour
    {
        [SerializeField] private float _maxY;
        [SerializeField] private float _minY;
        private bool _isClicked = false;
        private int _numSlotsUp;
        private float _percentOnY = 0;

        public float GetBarPercent() => _percentOnY;
        public bool IsClicked() => _isClicked;

        private void Update()
        {
            Vector3 localMousePosition = transform.parent.InverseTransformPoint(Input.mousePosition);

            if (localMousePosition.y > _minY && localMousePosition.y < _maxY && _isClicked)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, localMousePosition.y);
                _percentOnY = (_maxY - localMousePosition.y) / (_maxY - _minY);
            }

            if (localMousePosition.y < _minY && _isClicked)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, _minY);
                _percentOnY = 1;
            }


            if (localMousePosition.y > _maxY && _isClicked)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, _maxY);
                _percentOnY = 0;
            }
        }

        private void OnMouseUp()
        {
            _isClicked = false;
        }

        private void OnMouseDown()
        {
            _isClicked = true;
        }
    }
};
