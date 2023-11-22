namespace Game.Combat
{
    using UnityEngine;
    using System.Collections;
    using Game.Health;
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(ParticleSystem))]
    public class ProjectileController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private AudioSource _audio;
        private ParticleSystem _particle;
        private ProjectileSO _projectile;
        private HealthController _parentHealth;
        private Coroutine _flight;
        private bool _isFly = true;
        private bool _attackPlayer;
        private PlayerController _player;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            _particle = GetComponent<ParticleSystem>();
            _player = FindObjectOfType<PlayerController>();
        }
        private void Start()
        {
            _flight = StartCoroutine(FlightTime());
        }
        private void FixedUpdate()
        {
            if(_isFly == true)
                _rigidbody.MovePosition(_rigidbody.position + (Vector2)transform.right * _projectile.Speed * Time.deltaTime);
        }
        public void SetProjectile(HealthController parentHealth, ProjectileSO projectile, bool attackPlayer)
        {
            _parentHealth = parentHealth;
            _projectile = projectile;
            _attackPlayer = attackPlayer;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _isFly = false;
            _collider.enabled = false;
            _audio.Play();
            StopCoroutine(_flight);
            StartCoroutine(LifeTime());
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            HealthController health = collision.GetComponent<HealthController>();
            if (health == null || health == _parentHealth)
                return;
            if (_attackPlayer == true)
            {
                if (_player != null)
                {
                    if (_player.Health != health)
                        return;
                }
            }
            health.TakeDamage(_projectile.Damage, _projectile.Stun);
            Vector2 direction = health.transform.position - transform.position;
            collision.attachedRigidbody.AddForce(direction.normalized * _projectile.Knockback);
            if (_projectile.IsPenetrating == false)
            {
                if(_audio != null)
                    _audio.Play();
                _isFly = false;
                transform.parent = collision.transform;
                if (_particle != null)
                    _particle.Stop();
                _collider.enabled = false;
                StopCoroutine(_flight);
                StartCoroutine(LifeTime());
            }
        }
        private IEnumerator FlightTime()
        {
            yield return new WaitForSeconds(_projectile.FlightTime);
            _isFly = false;
            _collider.enabled = false;
            if (_audio != null)
                _audio.Play();
            StartCoroutine(LifeTime());
        }
        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(_projectile.LifeTime);
            Destroy(gameObject);
        }
    }
}