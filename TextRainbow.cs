using TMPro;
using UnityEngine;

namespace Agricosmic.Utilities
{
    /// <summary>
    /// Changes the color of text over the time in a rainbow!! :D
    /// ✧∘* ✧･ﾟ✧∘* ✧･ﾟ✧∘* ✧･ﾟ✧∘* ✧･ﾟ✧∘* ✧･ﾟ✧∘* ✧･ﾟ
    /// </summary>
    public class TextRainbow : MonoBehaviour
    {
        [Tooltip("The speed to at which to cycle through the colors")]
        [SerializeField] private float _speed = 0.2f;
        [Tooltip("Reference to the text to modify. Located automatically if null at start-up")]
        [SerializeField] private TMP_Text _text;
    
        private float _time;

        private void Start()
        {
            if (_text == null) _text = GetComponent<TMP_Text>();
        }

        // Update is called once per frame
        private void Update()
        {
            _time += Time.deltaTime * _speed;
            _text.color = Color.HSVToRGB(Mathf.Repeat(_time, 1f), 0.8f, 0.8f);
        }
    }

}
