namespace Game
{
    using UnityEngine;
    using Game.Health;
    [RequireComponent(typeof(FadeAnimation))]
    public class LooseTracker : MonoBehaviour
    {
        private HealthController _player;
        private FadeAnimation _fade;
        private bool _isOpened;
        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>().GetComponentInChildren<HealthController>();
            _fade = GetComponent<FadeAnimation>();
        }
        private void Update()
        {
            if (_isOpened == true)
                return;
            if (_player.IsDead == true)
            {
                _fade.FadeIn();
                _isOpened = true;
            }
        }
    }
}