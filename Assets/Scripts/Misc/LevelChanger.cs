namespace Game
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private string[] _levels;
        [SerializeField] private bool _loadOnAwake;
        private void Start()
        {
            if (_loadOnAwake == true)
                LoadRandom();
        }
        public void LoadRandom()
        {
            int index = Random.Range(0, _levels.Length);
            SceneManager.LoadScene(_levels[index]);
        }
    }
}