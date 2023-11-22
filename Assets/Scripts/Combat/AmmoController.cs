namespace Game.Combat
{
    using UnityEngine;
    public class AmmoController : MonoBehaviour
    {
        [SerializeField] private AmmoRenderer _renderer;
        [SerializeField] private int _maxAmmo;
        private int _currentAmmo;
        public bool IsEmpty { get; private set; }
        private void OnValidate()
        {
            if (_maxAmmo < 0)
                _maxAmmo = 0;
        }
        private void Start()
        {
            _currentAmmo = _maxAmmo;
            _renderer.UpdateAmmo(_currentAmmo);
        }
        public void AddAmmo(int count = 1)
        {
            _currentAmmo += count;
            IsEmpty = false;
            if (_renderer != null)
                _renderer.UpdateAmmo(_currentAmmo);
        }
        public void RemoveAmmo(int count = 1)
        {
            if (IsEmpty == true)
                return;
            _currentAmmo -= count;
            if (_currentAmmo <= 0)
                IsEmpty = true;
            if (_renderer != null)
                _renderer.UpdateAmmo(_currentAmmo);
        }
    }
}