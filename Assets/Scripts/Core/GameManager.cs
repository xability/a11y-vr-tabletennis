using UnityEngine;
using UnityEngine.SceneManagement;

namespace VRTableTennis.Core
{
    /// <summary>
    /// Central game manager that coordinates all game systems and manages game state
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        public bool isGameActive = false;
        public float gameStartDelay = 3f;
        
        [Header("References")]
        public GameObject player;
        public GameObject ball;
        public GameObject paddle;
        
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
                    }
                }
                return _instance;
            }
        }
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        public void StartGame()
        {
            isGameActive = true;
            Debug.Log("Game started");
        }
        
        public void PauseGame()
        {
            isGameActive = false;
            Debug.Log("Game paused");
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }
} 