namespace Game.Chase
{
    using UnityEngine;
    using System.Collections.Generic;
    [RequireComponent(typeof(Collider2D))]
    public class ObstacleHandler : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 2;
        [SerializeField] private int[] _layersToAvoid;
        private Collider2D _collider;
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        private void OnValidate()
        {
            if (_detectionRadius < 0)
                _detectionRadius = 0;
        }
        public float[] GetDangers()
        {
            float[] dangers = new float[ChaseDirections.Directions.Length];
            Collider2D[] obstacles = GetObstacles();
            foreach (var obstacle in obstacles)
            {
                Vector3 obstaclePosition = obstacle.ClosestPoint(_collider.bounds.center);
                Vector2 direction = obstaclePosition - _collider.bounds.center;
                float weight = 0;
                if (_detectionRadius > direction.magnitude)
                {
                    float t = Mathf.InverseLerp(0, _detectionRadius, direction.magnitude);
                    weight = Mathf.MoveTowards(1, 0, t);
                }
                for (int i = 0; i < dangers.Length; i++)
                {
                    float result = Vector2.Dot(direction.normalized, ChaseDirections.Directions[i]) * weight;
                    if (result > dangers[i])
                        dangers[i] = result;
                }
            }
            return dangers;
        }
        private Collider2D[] GetObstacles()
        {
            Collider2D[] obstacles = Physics2D.OverlapCircleAll(_collider.bounds.center, _detectionRadius);
            List<Collider2D> newObstacles = new List<Collider2D>();
            foreach (var obstacle in obstacles)
            {
                if (obstacle.isTrigger == true)
                    continue;
                if (CheckLayers(obstacle) == false)
                    continue;
                if (obstacle.GetComponent<AgentController>())
                    continue;
                newObstacles.Add(obstacle);
            }
            return newObstacles.ToArray();
        }
        private bool CheckLayers(Collider2D obstacle)
        {
            for (int i = 0; i < _layersToAvoid.Length; i++)
            {
                if (obstacle.gameObject.layer == _layersToAvoid[i])
                    return false;
            }
            return true;
        }
    }
}