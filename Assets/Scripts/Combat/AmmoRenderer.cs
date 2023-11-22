namespace Game.Combat
{
    using TMPro;
    using UnityEngine;
    public class AmmoRenderer : MonoBehaviour
    {
        private TextMeshProUGUI _textHolder;
        private void Awake()
        {
            _textHolder = GetComponentInChildren<TextMeshProUGUI>();
        }
        public void UpdateAmmo(int count)
        {
            _textHolder.text = count.ToString();
        }
    }
}