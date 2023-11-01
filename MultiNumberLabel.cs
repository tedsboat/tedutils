using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agricosmic.Utilities
{
    /// <summary>
    /// Contains multiple <see cref="NumberLabel"/>s and arranges them
    /// properly in space to form multi-digit numbers
    /// </summary>
    public class MultiNumberLabel : MonoBehaviour
    {
        [SerializeField] private int _value;
        [SerializeField] private bool _useSign = false;
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private bool _isOutlined;
        [SerializeField] private GameObject _numberLabelPrefab;
        [SerializeField] private Sprite[] _plusAndMinusSprites;
        [SerializeField] private float _numberSpacing;
        [SerializeField] private SpriteRenderer _renderer;
        readonly List<GameObject> _children = new();

        private void Start()
        {
            if (_renderer == null) _renderer = GetComponent<SpriteRenderer>();
        }

        private string _absValueString => Mathf.Abs(_value).ToString();

        private void UpdateNumberLabels()
        {
            Transform thisTransform = transform;
    
            foreach (GameObject child in _children)
            {
                Destroy(child);
            }
            
            _children.Clear();

            for (int i = 0; i < _absValueString.Length; i++)
            {
                _children.Add(Instantiate(_numberLabelPrefab, new Vector3((thisTransform.position.x -(_numberSpacing * i) + _numberSpacing * _absValueString.Length), 
                    thisTransform.position.y, -10), Quaternion.identity, thisTransform));
    
                _children[i].GetComponent<SpriteRenderer>().material = _renderer.material;
                
                int tempValue = Mathf.Abs(_value) % (int) Math.Pow(10.0f, i + 1);
                NumberLabel childLabel = _children[i].GetComponent<NumberLabel>();
                childLabel.SetValue(tempValue / (int)Mathf.Pow(10, i));
                childLabel.SetOutlined(_isOutlined);
                childLabel.SetColor(_color);
                childLabel.ShowLabel();
                childLabel.GetComponent<SpriteRenderer>().sortingLayerName =
                    _renderer.sortingLayerName;
            }
            
            // Add sign indicators
            if (_useSign)
            {
                var firstCharacterPosition = _children[0].transform.localPosition;
                var signIndicator = new GameObject("Sign Indicator");
                signIndicator.transform.parent = thisTransform;
                signIndicator.transform.localPosition = firstCharacterPosition + Vector3.left * _numberSpacing;
                
                var signRenderer = signIndicator.AddComponent<SpriteRenderer>();
                signRenderer.sprite = _plusAndMinusSprites[_value > 0 ? 0 : 1];
                signRenderer.color = _color;
                signRenderer.sortingLayerName = _renderer.sortingLayerName;
                signRenderer.material = _renderer.material;
                
                _children.Add(signIndicator);
            }
        }
    
        /// <summary>
        /// Hides the numbers
        /// </summary>
        public void HideLabel() => gameObject.SetActive(false);
        
        /// <summary>
        /// Shows the numbers
        /// </summary>
        public void ShowLabel()
        {
            UpdateNumberLabels();
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Changes the value to display
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(int value, bool useSign = false)
        {
            _value = value;
            _useSign = useSign;
            UpdateNumberLabels();
        }

        public void SetColor(Color color)
        {
            _color = color;
            UpdateNumberLabels();
        }
        
        public void SetOpacity(float opacity)
        {
            foreach (var child in _children)
            {
                var spriteRenderer = child.GetComponent<SpriteRenderer>();
                var color = spriteRenderer.color;
                color.a = opacity;
                spriteRenderer.color = color;

            }
        }
    }

}
