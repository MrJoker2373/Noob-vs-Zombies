namespace Game
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.Events;
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeAnimation : MonoBehaviour
    {
        [SerializeField] private bool _fadeInOnAwake;
        [SerializeField] private bool _fadeOutOnAwake;
        [SerializeField] private UnityEvent _onFadeIn;
        [SerializeField] private UnityEvent _onFadeOut;
        [SerializeField] private float _speed;
        private CanvasGroup _group;
        private void Awake()
        {
            _group = GetComponent<CanvasGroup>();
        }
        private void OnValidate()
        {
            if (_speed < 0)
                _speed = 0;
        }
        private void Start()
        {
            if (_fadeInOnAwake == true)
                FadeIn();
            else if (_fadeOutOnAwake == true)
                FadeOut();
        }
        public void FadeIn()
        {
            StopAllCoroutines();
            StartCoroutine(FadeInCoroutine());
        }
        public void FadeOut()
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutCoroutine());
        }
        private IEnumerator FadeInCoroutine()
        {
            _group.interactable = true;
            _group.blocksRaycasts = true;
            while (_group.alpha != 1f)
            {
                _group.alpha = Mathf.MoveTowards(_group.alpha, 1, _speed * Time.deltaTime);
                yield return null;
            }
            _onFadeIn.Invoke();
        }
        private IEnumerator FadeOutCoroutine()
        {
            _group.interactable = false;
            _group.blocksRaycasts = false;
            while (_group.alpha != 0f)
            {
                _group.alpha = Mathf.MoveTowards(_group.alpha, 0, _speed * Time.deltaTime);
                yield return null;
            }
            _onFadeOut.Invoke();
        }
    }
}