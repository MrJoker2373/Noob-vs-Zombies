namespace Game.Health
{
    using UnityEngine;
    using UnityEngine.UI;
    [RequireComponent(typeof(Image))]
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Sprite _healthEmpty;
        [SerializeField] private Sprite _healthHalf;
        [SerializeField] private Sprite _healthFull;
        [SerializeField] private Sprite _extraHealthHalf;
        [SerializeField] private Sprite _extraHealthFull;
        private Image _image;
        private void Awake()
        {
            _image = GetComponent<Image>();
        }
        public void SetHeart(HealthType type)
        {
            switch (type)
            {
                case HealthType.HealthEmpty:
                    _image.sprite = _healthEmpty;
                    break;
                case HealthType.HealthHalf:
                    _image.sprite = _healthHalf;
                    break;
                case HealthType.HealthFull:
                    _image.sprite = _healthFull;
                    break;
                case HealthType.ExtraHealthHalf:
                    _image.sprite = _extraHealthHalf;
                    break;
                case HealthType.ExtraHealthFull:
                    _image.sprite = _extraHealthFull;
                    break;
            }
        }
    }
    public enum HealthType { HealthEmpty, HealthHalf, HealthFull, ExtraHealthHalf, ExtraHealthFull }
}