namespace Game
{
    using UnityEngine;
    using Game.Chase;
    using Game.Health;
    using Game.Combat;
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(ChaseController))]
    public class EnemyController : MonoBehaviour
    {
        private PlayerController _player;
        private MovementController _movement;
        private ChaseController _chase;
        private HealthController _health;
        private WeaponController _weapon;
        private WeaponParent _parent;
        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
            _movement = GetComponent<MovementController>();
            _chase = GetComponent<ChaseController>();
            _health = GetComponentInChildren<HealthController>();
            _weapon = GetComponentInChildren<WeaponController>();
            _parent = GetComponentInChildren<WeaponParent>();
        }
        private void Start()
        {
            if (_player != null)
                _chase.SetTarget(_player.transform);
        }
        private void Update()
        {
            if (_player == null || _weapon == null)
            {
                _movement.SetDirection(Vector2.zero);
                return;
            }
            if (_health.IsDead == true || _health.IsStunned == true)
            {
                _movement.SetDirection(Vector2.zero);
                return;
            }
            if (_parent != null)
            {
                Vector2 parentdirection = _player.transform.position - transform.position;
                _parent.SetDirection(parentdirection.normalized);
            }
            if (_weapon.IsAnimating == true)
                _movement.SetDirection(Vector2.zero);
            else
            {
                Vector2 chaseDirection = _chase.GetChaseDirection();
                _movement.SetDirection(chaseDirection);
                if (_chase.IsReached && _chase.IsChasing)
                    _weapon.TryAttack();
            }
        }
    }
}