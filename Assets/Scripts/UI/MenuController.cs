using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRTableTennis.Core;

namespace VRTableTennis.UI
{
    /// <summary>
    /// Handles menu navigation and scene management for the VR Table Tennis game
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        [Header("Scene Settings")]
        public string _newGameLevel;
        private string levelToLoad;
        
        [Header("UI References")]
        [SerializeField] private GameObject noSavedGameDialog = null;

        public void NewGameDialogYes()
        {
            SceneManager.LoadScene(_newGameLevel);
        }

        public void LoadGameDialogYes()
        {
            if (PlayerPrefs.HasKey("SavedLevel"))
            {
                levelToLoad = PlayerPrefs.GetString("SavedLevel");
                SceneManager.LoadScene(levelToLoad);
            }
            else
            {
                noSavedGameDialog.SetActive(true);
            }
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
} 