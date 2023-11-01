using Agricosmic.Farm;
using UnityEngine;

namespace Agricosmic.Utilities
{
    /// <summary>
    /// DEPRECATED - Moves its parent based on its vertical position
    /// <br/>
    /// TODO: @xavier needs rework :)
    /// </summary>
    public class HorizontalScrollBar : MonoBehaviour
    {
        [SerializeField] private FarmCursor _cursor;
        public float _maxX;
        public float _minX;
        private bool _isClicked = false;
        private float _percentOnX = 0;

        public float GetBarPercent() => _percentOnX;
        public bool IsClicked() => _isClicked;

        private void Update()
        {
            Vector3 localMousePosition = transform.parent.InverseTransformPoint(_cursor.GetMousePosition());

            if (localMousePosition.x > _minX && localMousePosition.x < _maxX && _isClicked)
            {
                transform.localPosition = new Vector2(localMousePosition.x, transform.localPosition.y);
                _percentOnX = (_maxX - localMousePosition.x) / (_maxX - _minX);
            }

            if (localMousePosition.x < _minX && _isClicked)
            {
                transform.localPosition = new Vector2(_minX, transform.localPosition.y);
                _percentOnX = 1;
            }


            if (localMousePosition.x > _maxX && _isClicked)
            {
                transform.localPosition = new Vector2(_maxX, transform.localPosition.y);
                _percentOnX = 0;
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
