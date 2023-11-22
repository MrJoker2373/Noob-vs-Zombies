namespace Game.Chase
{
    using UnityEngine;
    [RequireComponent(typeof(Collider2D))]
    public class TargetHandler : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 5;
        [SerializeField] private float _deadRadius = 1;
        private Collider2D _collider;
        private Collider2D _target;
        private Vector3 _lastPosition;
        public bool IsChasing { get; private set; }
        public bool IsReached { get; private set; }
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        private void OnValidate()
        {
            if (_detectionRadius < 0)
                _detectionRadius = 0;
        }
        public void SetTarget(Collider2D target) => _target = target;
        public float[] GetInterests()
        {
            float[] interests = new float[ChaseDirections.Directions.Length];
            if (CheckTarget() == true)
            {
                IsChasing = true;
                _lastPosition = _target.ClosestPoint(_collider.bounds.center);
            }
            else
                IsChasing = false;
            Vector2 direction = _lastPosition - _collider.bounds.center;
            IsReached = direction.magnitude <= _deadRadius;
            if (IsReached == true)
                return interests;
            for (int i = 0; i < interests.Length; i++)
            {
                float result = Vector2.Dot(direction.normalized, ChaseDirections.Directions[i]);
                if (result > interests[i])
                    interests[i] = result;
            }
            return interests;
        }
        private bool CheckTarget()
        {
            if (_target == null)
                return false;
            Vector2 direction = _target.bounds.center - _collider.bounds.center;
            if (direction.magnitude > _detectionRadius)
                return false;
            RaycastHit2D[] hits = Physics2D.RaycastAll(_collider.bounds.center, direction.normalized, _detectionRadius);
            foreach (var hit in hits)
            {
                if (hit.collider == null)
                    continue;
                if (hit.collider.isTrigger == true || hit.collider == _collider)
                    continue;
                if (hit.collider != _target)
                    break;
                return true;
            }
            return false;
        }
    }
}