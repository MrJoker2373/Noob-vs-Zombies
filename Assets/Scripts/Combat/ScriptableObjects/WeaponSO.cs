namespace Game.Combat
{
    using UnityEngine;
    public abstract class WeaponSO : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private float _delay;
        [SerializeField] private float _cooldown;
        public Sprite Sprite => _sprite;
        public float Delay => _delay;
        public float Cooldown => _cooldown;
        protected virtual void OnValidate()
        {
            if (_delay < 0)
                _delay = 0;
            if (_cooldown < 0)
                _cooldown = 0;
        }
    }
}