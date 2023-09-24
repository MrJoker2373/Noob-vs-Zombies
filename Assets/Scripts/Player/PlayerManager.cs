namespace Game.Player
{
    using UnityEngine;
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAnimation))]
    public class PlayerManager : MonoBehaviour
    {
        private PlayerMovement _movement;
        private PlayerAnimation _animation;
        public PlayerMovement Movement => _movement;
        public PlayerAnimation Animation => _animation;
        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _animation = GetComponent<PlayerAnimation>();
        }
    }
}