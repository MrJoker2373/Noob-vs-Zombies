namespace Game.Combat
{
    using UnityEngine;
    [CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Game/Melee Weapon")]
    public class MeleeWeaponSO : WeaponSO
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _knockback;
        [SerializeField] private float _stun;
        [SerializeField] private float _radius;
        [SerializeField] private bool _isPenetrating;
        public float Damage => _damage;
        public float Knockback => _knockback;
        public float Stun => _stun;
        public float Radius => _radius;
        public bool IsPenetrating => _isPenetrating;
        protected override void OnValidate()
        {
            base.OnValidate();
            if (_damage < 0)
                _damage = 0;
            if (_knockback < 0)
                _knockback = 0;
            if (_stun < 0)
                _stun = 0;
            if (_radius < 0)
                _radius = 0;
        }
    }
}