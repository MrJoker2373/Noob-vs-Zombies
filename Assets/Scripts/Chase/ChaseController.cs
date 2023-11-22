namespace Game.Chase
{
    using UnityEngine;
    [RequireComponent(typeof(ObstacleHandler))]
    [RequireComponent(typeof(TargetHandler))]
    public class ChaseController : MonoBehaviour
    {
        private ObstacleHandler _obstacle;
        private TargetHandler _target;
        public bool IsReached => _target.IsReached;
        public bool IsChasing => _target.IsChasing;
        private void Awake()
        {
            _obstacle = GetComponent<ObstacleHandler>();
            _target = GetComponent<TargetHandler>();
        }
        public void SetTarget(Transform target)
        {
            Collider2D collider = target.GetComponent<Collider2D>();
            if (collider != null)
                _target.SetTarget(collider);
        }
        public Vector2 GetChaseDirection()
        {
            float[] dangers = _obstacle.GetDangers();
            float[] interests = _target.GetInterests();
            float[] results = new float[ChaseDirections.Directions.Length];
            for (int i = 0; i < results.Length; i++)
                results[i] = Mathf.Clamp01(interests[i] - dangers[i]);
            Vector2 chaseDirection = Vector2.zero;
            for (int i = 0; i < ChaseDirections.Directions.Length; i++)
                chaseDirection += ChaseDirections.Directions[i] * results[i];
            if (_target.IsChasing == false)
                return Vector2.zero;
            else
                return chaseDirection.normalized;
        }
    }
    public static class ChaseDirections
    {
        public static readonly Vector2[] Directions =
        {
            new Vector2(0, 1).normalized,
            new Vector2(1, 1).normalized,
            new Vector2(1, 0).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(0, -1).normalized,
            new Vector2(-1, -1).normalized,
            new Vector2(-1, 0).normalized,
            new Vector2(-1, 1).normalized,
        };
    }
}