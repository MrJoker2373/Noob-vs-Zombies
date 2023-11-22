namespace Game.Health
{
    using UnityEngine;
    using System.Collections;
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(ParticleSystem))]
    [RequireComponent(typeof(ColorController))]
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private HealthRenderer _renderer;
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _extraHealth;
        [SerializeField] private float _invincibilityTime;
        private AudioSource _audio;
        private ParticleSystem _particle;
        private ColorController _color;
        private Animator _animator;
        private bool _isInvincible;
        public bool IsStunned { get; private set; }
        public bool IsDead { get; private set; }
        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _particle = GetComponent<ParticleSystem>();
            _color = GetComponent<ColorController>();
            _animator = GetComponentInParent<Animator>();
        }
        private void OnValidate()
        {
            if (_maxHealth < 0)
                _maxHealth = 0;
            if (_currentHealth < 0)
                _currentHealth = 0;
            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
            if (_extraHealth < 0)
                _extraHealth = 0;
            if (_invincibilityTime < 0)
                _invincibilityTime = 0;
        }
        private void Start()
        {
            if (_renderer != null)
                _renderer.UpdateHealth(_maxHealth, _currentHealth, _extraHealth);
        }
        public void AddContainer(float health = 0.5f)
        {
            if (IsDead == true)
                return;
            _maxHealth += (int)ClampHealth(health);
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            if (_renderer != null)
                _renderer.UpdateHealth(_maxHealth, _currentHealth, _extraHealth);
        }
        public void RemoveContainer(float health = 0.5f)
        {
            if (IsDead == true)
                return;
            _maxHealth -= (int)ClampHealth(health);
            _maxHealth = Mathf.Clamp(_maxHealth, 0, int.MaxValue);
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            if (_renderer != null)
                _renderer.UpdateHealth(_maxHealth, _currentHealth, _extraHealth);
        }
        public void AddHealth(float health = 0.5f)
        {
            if (IsDead == true)
                return;
            _currentHealth += ClampHealth(health);
            if (_renderer != null)
                _renderer.UpdateHealth(_maxHealth, _currentHealth, _extraHealth);
        }
        public void AddExtraHealth(float health = 0.5f)
        {
            if (IsDead == true)
                return;
            _extraHealth += ClampHealth(health);
            if (_renderer != null)
                _renderer.UpdateHealth(_maxHealth, _currentHealth, _extraHealth);
        }
        public void TakeDamage(float health = 0.5f, float stunTime = 0f)
        {
            if (_isInvincible == true || IsStunned == true || IsDead == true)
                return;
            RemoveHealth(health);
            _audio.Play();
            _particle.Play();
            if (_currentHealth <= 0)
                Kill();
            else
            {
                StartCoroutine(Invincibility());
                StartCoroutine(Stun(stunTime));
                _color.SmoothChangeColor(Color.red);
            }
        }
        public void Kill()
        {
            if (IsDead == true)
                return;
            IsDead = true;
            _currentHealth = 0;
            StopAllCoroutines();
            _color.ChangeColor(Color.red);
            _animator.SetTrigger("Death");
            if (_renderer != null)
                _renderer.DisableHealth();
        }
        private float ClampHealth(float health)
        {
            int integer = Mathf.FloorToInt(health);
            float fraction = health - integer;
            if (fraction != 0f)
                fraction = 0.5f;
            return integer + fraction;
        }
        private void RemoveHealth(float health)
        {
            float clampedHealth = ClampHealth(health);
            if (_extraHealth > 0)
            {
                float remainder = clampedHealth - _extraHealth;
                _extraHealth -= clampedHealth;
                if (remainder > 0)
                    _currentHealth -= remainder;
            }
            else
                _currentHealth -= clampedHealth;
            if (_renderer != null)
                _renderer.UpdateHealth(_maxHealth, _currentHealth, _extraHealth);
        }
        private IEnumerator Invincibility()
        {
            _isInvincible = true;
            yield return new WaitForSeconds(_invincibilityTime);
            _isInvincible = false;
        }
        private IEnumerator Stun(float duration)
        {
            IsStunned = true;
            yield return new WaitForSeconds(duration);
            IsStunned = false;
        }
    }
}