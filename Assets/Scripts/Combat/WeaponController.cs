namespace Game.Combat
{
    using UnityEngine;
    using System.Collections;
    using Game.Health;
    public abstract class WeaponController : MonoBehaviour
    {
        [SerializeField] protected Transform _attackPoint;
        [SerializeField] protected WeaponSO _weapon;
        [SerializeField] private AudioSource _audio;
        protected HealthController _health;
        protected Animator _animator;
        protected SpriteRenderer _sprite;
        private AmmoController _ammo;
        public bool IsAnimating { get; private set; }
        public bool IsAttacking { get; private set; }
        protected virtual void Awake()
        {
            _animator = GetComponentInParent<Animator>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
            _ammo = GetComponent<AmmoController>();
            AgentController agent = GetComponentInParent<AgentController>();
            _health = agent.GetComponentInChildren<HealthController>();
        }
        public void TryAttack()
        {
            if (_attackPoint == null || _weapon == null || _audio == null)
                return;
            if (IsAttacking == true || _health.IsStunned == true || _health.IsDead == true)
                return;
            if (_ammo != null)
            {
                if(_ammo.IsEmpty == true)
                    return;
                _ammo.RemoveAmmo();
            }
            IsAttacking = true;
            IsAnimating = true;
            StartCoroutine(Delay());
        }
        protected abstract void Attack();
        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(_weapon.Delay);
            _animator.SetTrigger("Attack");
            if(_audio != null)
                _audio.Play();
        }
        private IEnumerator Cooldown()
        {
            Attack();
            IsAnimating = false;
            yield return new WaitForSeconds(_weapon.Cooldown);
            IsAttacking = false;
        }
    }
}