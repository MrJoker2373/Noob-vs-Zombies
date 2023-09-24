namespace Game.Core
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        private InputActionAsset _inputActions;
        private PlayerInput _playerInput;
        public string ActionMap
        {
            get { return _playerInput.currentActionMap.name; }
            set
            {
                for (int i = 0; i < _inputActions.actionMaps.Count; i++)
                {
                    if (_inputActions.actionMaps[i].name.ToUpper() == value.ToUpper())
                        _playerInput.currentActionMap = _inputActions.actionMaps[i];
                }
            }
        }
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _inputActions = _playerInput.actions;
        }
    }
}