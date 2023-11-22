namespace Game.Combat
{
    using UnityEngine;
    public class KamikazeWeapon : MeleeWeapon
    {
        [SerializeField] private GameObject _explosionPrefab;
        protected override void Attack()
        {
            base.Attack();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }
}