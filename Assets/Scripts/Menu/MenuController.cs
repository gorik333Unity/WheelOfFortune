using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private Button _startGameButton;

        [SerializeField]
        private string _gameSceneName;

        private void Awake()
        {
            _startGameButton.onClick.RemoveListener(StartGameButton_OnClick);
            _startGameButton.onClick.AddListener(StartGameButton_OnClick);
        }

        private void StartGameButton_OnClick()
        {
            SceneManager.LoadScene(_gameSceneName);
        }
    }
}