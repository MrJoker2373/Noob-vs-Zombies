namespace Game
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        private PlayerController _player;
        private PlayerInput _input;
        private bool _isDisabled;
        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
            _input = GetComponent<PlayerInput>();
        }
        public void DisableInput()
        {
            _input.DeactivateInput();
            _isDisabled = true;
        }
        private void Update()
        {
            if (_isDisabled == true)
                return;
            if (_player.Health.IsDead || _player.Health.IsStunned || _player.Changer.IsAnimating)
                _input.DeactivateInput();
            else
                _input.ActivateInput();
        }
        public void OnMovement(InputAction.CallbackContext context)
        {
            if (_player == null)
                return;
            Vector2 direction = context.ReadValue<Vector2>();
            _player.Movement.SetDirection(direction);
        }
        public void OnMousePosition(InputAction.CallbackContext context)
        {
            if (_player == null)
                return;
            if (context.phase == InputActionPhase.Canceled)
                return;
            Vector2 position = context.ReadValue<Vector2>();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            Vector2 offset = worldPosition - _player.transform.position;
            _player.Parent.SetDirection(offset);
        }
        public void OnAttack()
        {
            if (_player == null)
                return;
            _player.Changer.TryAttack();
        }
        public void OnChangeWeapon()
        {
            if (_player == null)
                return;
            _player.Changer.ChangeWeapon();
        }
    }
}