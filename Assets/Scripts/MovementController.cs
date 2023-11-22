namespace Game
{
    using UnityEngine;
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(FlipController))]
    [RequireComponent(typeof(AudioSource))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private bool _canFlip = true;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private FlipController _flip;
        private AudioSource _audio;
        private Vector2 _direction;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _flip = GetComponent<FlipController>();
            _audio = GetComponent<AudioSource>();
        }
        private void OnValidate()
        {
            if (_speed < 0)
                _speed = 0;
        }
        private void FixedUpdate()
        {
            _rigidbody.velocity += _direction * _speed;
        }
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
            _animator.SetBool("IsMoving", direction.magnitude != 0);
            if (_canFlip == true)
                _flip.SetFlip(direction);
        }
        private void PlayAudio() => _audio.Play();
    }
}