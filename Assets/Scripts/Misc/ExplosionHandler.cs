namespace Game
{
    using UnityEngine;
    public class ExplosionHandler : MonoBehaviour
    {
        private void HandleExplosion() => Destroy(gameObject);
    }
}