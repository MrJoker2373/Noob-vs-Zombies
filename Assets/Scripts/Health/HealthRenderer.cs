namespace Game.Health
{
    using UnityEngine;
    public class HealthRenderer : MonoBehaviour
    {
        [SerializeField] private GameObject _heartPrefab;
        private Canvas _canvas;
        private bool _isDisabled;
        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
        }
        public void DisableHealth()
        {
            if (_isDisabled == true)
                return;
            DestroyHealth();
            _isDisabled = true;
            _canvas.enabled = false;
        }
        public void UpdateHealth(int maxHealth, float currentHealth, float extraHealth)
        {
            if (_isDisabled == true)
                return;
            DestroyHealth();
            for (int i = 0; i < maxHealth; i++)
            {
                HealthUI health = CreateHealth();
                int integer = Mathf.FloorToInt(currentHealth);
                float fraction = currentHealth - integer;
                if (integer >= i + 1)
                    health.SetHeart(HealthType.HealthFull);
                else
                {
                    if (integer == i && fraction != 0)
                        health.SetHeart(HealthType.HealthHalf);
                    else
                        health.SetHeart(HealthType.HealthEmpty);
                }
            }
            for (int i = 0; i < extraHealth; i++)
            {
                HealthUI health = CreateHealth();
                if (extraHealth >= i + 1)
                    health.SetHeart(HealthType.ExtraHealthFull);
                else
                    health.SetHeart(HealthType.ExtraHealthHalf);
            }
        }
        private HealthUI CreateHealth()
        {
            GameObject newObject = Instantiate(_heartPrefab, transform);
            newObject.transform.localScale = Vector3.one;
            return newObject.GetComponent<HealthUI>();
        }
        private void DestroyHealth()
        {
            for (int i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }
    }
}