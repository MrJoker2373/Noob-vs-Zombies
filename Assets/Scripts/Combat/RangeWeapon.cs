namespace Game.Combat
{
    using UnityEngine;
    public class RangeWeapon : WeaponController
    {
        [SerializeField] private bool _attackPlayer;
        [SerializeField] private GameObject _projectilePrefab;
        private RangeWeaponSO _rangeWeapon;
        protected override void Awake()
        {
            base.Awake();
            _rangeWeapon = _weapon as RangeWeaponSO;
        }
        private void Start()
        {
            if (_weapon != null)
                SetWeapon(_rangeWeapon);
        }
        public void SetWeapon(RangeWeaponSO weapon)
        {
            _weapon = weapon;
            _sprite.sprite = weapon.Sprite;
        }
        protected override void Attack()
        {
            GameObject projectile = Instantiate(_projectilePrefab, _attackPoint.position, Quaternion.identity);
            projectile.transform.right = transform.right * _health.transform.parent.localScale.normalized.x;
            projectile.GetComponent<ProjectileController>().SetProjectile(_health, _rangeWeapon.Projectile, _attackPlayer);
        }
    }
}