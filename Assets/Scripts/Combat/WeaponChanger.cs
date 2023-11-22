namespace Game.Combat
{
    using UnityEngine;
    public class WeaponChanger : MonoBehaviour
    {
        private MeleeWeapon _melee;
        private RangeWeapon _range;
        private bool _isMelee;
        public bool IsAnimating
        {
            get
            {
                if (_isMelee == true)
                    return _melee.IsAnimating;
                else
                    return _range.IsAnimating;
            }
        }
        private void Awake()
        {
            _melee = GetComponentInChildren<MeleeWeapon>(true);
            _range = GetComponentInChildren<RangeWeapon>(true);
        }
        private void Start()
        {
            _isMelee = true;
            _range.gameObject.SetActive(false);
            _melee.gameObject.SetActive(true);
        }
        public void ChangeWeapon()
        {
            if (_isMelee == false)
            {
                if (_range.IsAttacking == true)
                    return;
                _isMelee = true;
                _range.gameObject.SetActive(false);
                _melee.gameObject.SetActive(true);
            }
            else
            {
                if (_melee.IsAttacking == true)
                    return;
                _isMelee = false;
                _range.gameObject.SetActive(true);
                _melee.gameObject.SetActive(false);
            }
        }
        public void TryAttack()
        {
            if (_isMelee == false)
                _range.TryAttack();
            else
                _melee.TryAttack();
        }
    }
}