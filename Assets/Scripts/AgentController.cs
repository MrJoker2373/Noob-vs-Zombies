namespace Game
{
    using UnityEngine;
    public class AgentController : MonoBehaviour
    {
        private void HandleDeath() => Destroy(transform.parent.gameObject);
    }
}