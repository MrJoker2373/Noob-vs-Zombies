namespace Game
{
    using UnityEngine;
    [RequireComponent(typeof(FadeAnimation))]
    public class WinTracker : MonoBehaviour
    {
        private EnemySpawner[] _spawners;
        private InputReader _input;
        private FadeAnimation _fade;
        private bool _isOpened;
        private void Awake()
        {
            _spawners = FindObjectsOfType<EnemySpawner>();
            _input = FindObjectOfType<InputReader>();
            _fade = GetComponent<FadeAnimation>();
        }
        private void Update()
        {
            if (_isOpened == true)
                return;
            for (int i = 0; i < _spawners.Length; i++)
            {
                if (_spawners[i].IsEmpty == false)
                    return;
            }
            EnemyController enemy = FindObjectOfType<EnemyController>();
            if (enemy == null)
            {
                _fade.FadeIn();
                _input.DisableInput();
                _isOpened = true;
                AdShower.ShowAd();
            }
        }
    }
}