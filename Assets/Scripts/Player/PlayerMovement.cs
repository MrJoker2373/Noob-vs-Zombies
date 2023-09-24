namespace Game.Player
{
    using UnityEngine;
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidBody;
        private Vector2 _direction;
        public Vector2 Direction { set { _direction = value; } }
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();   
        }
        private void FixedUpdate()
        {
            _rigidBody.velocity = _direction * _speed; 
        }
    }
}