namespace Game.Combat
{
    using UnityEngine;
    public class WeaponParent : MonoBehaviour
    {
        private FlipController _flip;
        private void Awake()
        {
            _flip = GetComponentInParent<FlipController>();
        }
        public void SetDirection(Vector2 direction)
        {
            if (_flip == null)
                return;
            transform.right = direction * _flip.transform.localScale.normalized.x;
            _flip.SetFlip(direction);
        }
    }
}