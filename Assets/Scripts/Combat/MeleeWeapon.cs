namespace Game.Combat
{
    using UnityEngine;
    using Game.Health;
    public class MeleeWeapon : WeaponController
    {
        [SerializeField] private bool _attackPlayer;
        private MeleeWeaponSO _meleeWeapon;
        private PlayerController _player;
        protected override void Awake()
        {
            base.Awake();
            _meleeWeapon = _weapon as MeleeWeaponSO;
            _player = FindObjectOfType<PlayerController>();
        }
        private void Start()
        {
            if (_meleeWeapon != null)
                SetWeapon(_meleeWeapon);
        }
        public void SetWeapon(MeleeWeaponSO weapon)
        {
            _weapon = weapon;
            if(weapon.Sprite != null)
                _sprite.sprite = weapon.Sprite;
        }
        protected override void Attack()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, _meleeWeapon.Radius);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].isTrigger == false)
                    continue;
                HealthController health = colliders[i].GetComponent<HealthController>();
                if (health == null || health == _health)
                    continue;
                if (_attackPlayer == true)
                {
                    if (_player.Health != health)
                        return;
                }
                health.TakeDamage(_meleeWeapon.Damage, _meleeWeapon.Stun);
                Knockback(health);
                if (_meleeWeapon.IsPenetrating == false)
                    return;
            }
        }
        private void Knockback(HealthController health)
        {
            if (_weapon is MeleeWeaponSO weapon)
            {
                Rigidbody2D rigidbody = health.GetComponentInParent<Rigidbody2D>();
                if (rigidbody == null)
                    return;
                Vector2 direction = health.transform.position - _health.transform.position;
                rigidbody.AddForce(direction.normalized * weapon.Knockback);
            }
        }
        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null || _meleeWeapon == null)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_attackPoint.position, _meleeWeapon.Radius);
        }
    }
}