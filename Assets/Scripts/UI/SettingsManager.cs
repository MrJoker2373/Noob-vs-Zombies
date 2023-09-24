namespace Game.UI
{
    using UnityEngine;
    using Game.Core;
    public class SettingsManager : MonoBehaviour
    {
        private InputManager _inputManager;
        private string _currentActionMap;
        private void Awake()
        {
            _inputManager = FindObjectOfType<InputManager>();
        }
        public void Open()
        {
            Time.timeScale = 0;
            _currentActionMap = _inputManager.ActionMap;
            _inputManager.ActionMap = "None";
        }
        public void Close()
        {
            Time.timeScale = 1;
            _inputManager.ActionMap = _currentActionMap;
        }
    }
}