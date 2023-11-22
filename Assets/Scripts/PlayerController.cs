namespace Game
{
    using UnityEngine;
    using Game.Combat;
    using Game.Health;
    [RequireComponent(typeof(MovementController))]
    public class PlayerController : AgentController
    {
        public MovementController Movement { get; private set; }
        public WeaponParent Parent { get; private set; }
        public WeaponChanger Changer { get; private set; }
        public HealthController Health { get; private set; }
        private void Awake()
        {
            Movement = GetComponent<MovementController>();
            Parent = GetComponentInChildren<WeaponParent>();
            Changer = GetComponentInChildren<WeaponChanger>();   
            Health = GetComponentInChildren<HealthController>();
        }
    }
}