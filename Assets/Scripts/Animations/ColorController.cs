namespace Game
{
    using UnityEngine;
    using System.Collections;
    public class ColorController : MonoBehaviour
    {
        [SerializeField] private float _inSpeed;
        [SerializeField] private float _outSpeed;
        [SerializeField] private float _delay;
        [SerializeField] private SpriteRenderer[] _sprites;
        private Color _startColor;
        private void OnValidate()
        {
            if (_inSpeed < 0)
                _inSpeed = 0;
            if (_outSpeed < 0)
                _outSpeed = 0;
            if (_delay < 0)
                _delay = 0;
        }
        private void Start()
        {
            _startColor = _sprites[0].color;
        }
        public void SmoothChangeColor(Color color)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothChangeAnimation(color));
        }
        public void ChangeColor(Color color)
        {
            StopAllCoroutines();
            SetColor(color);
        }
        private void SetColor(Color color)
        {
            for (int i = 0; i < _sprites.Length; i++)
                _sprites[i].color = color;
        }
        private IEnumerator SmoothChangeAnimation(Color color)
        {
            float t = 0;
            while (t != 1f)
            {
                t = Mathf.MoveTowards(t, 1f, Time.deltaTime * _inSpeed);
                Color newColor = Color.Lerp(_sprites[0].color, color, t);
                SetColor(newColor);
                yield return null;
            }
            SetColor(color);
            yield return new WaitForSeconds(_delay);
            while (t != 0f)
            {
                t = Mathf.MoveTowards(t, 0f, Time.deltaTime * _outSpeed);
                Color newColor = Color.Lerp(_startColor, Color.white, t);
                SetColor(newColor);
                yield return null;
            }
            SetColor(_startColor);
        }
    }
}