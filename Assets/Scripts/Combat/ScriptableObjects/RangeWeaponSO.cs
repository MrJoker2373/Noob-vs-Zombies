namespace Game.Combat
{
    using UnityEngine;
    [CreateAssetMenu(fileName = "New Range Weapon", menuName = "Game/Range Weapon")]
    public class RangeWeaponSO : WeaponSO
    {
        [SerializeField] private ProjectileSO _projectile;
        public ProjectileSO Projectile => _projectile;
    }
}