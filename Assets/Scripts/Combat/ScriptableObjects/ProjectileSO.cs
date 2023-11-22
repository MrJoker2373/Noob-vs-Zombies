namespace Game.Combat
{
    using UnityEngine;
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Game/Projectile")]
    public class ProjectileSO : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private float _damage;
        [SerializeField] private float _knockback;
        [SerializeField] private float _stun;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _flightTime;
        [SerializeField] private float _speed;
        [SerializeField] private bool _isPenetrating;
        public Sprite Sprite => _sprite;
        public float Damage => _damage;
        public float Knockback => _knockback;
        public float Stun => _stun;
        public float LifeTime => _lifeTime;
        public float FlightTime => _flightTime;  
        public float Speed => _speed;
        public bool IsPenetrating => _isPenetrating;
        private void OnValidate()
        {
            if (_damage < 0)
                _damage = 0;
            if (_knockback < 0)
                _knockback = 0;
            if (_stun < 0)
                _stun = 0;
            if (_lifeTime < 0)
                _lifeTime = 0;
            if (_flightTime < 0)
                _flightTime = 0;
            if (_speed < 0)
                _speed = 0;
        }
    }
}