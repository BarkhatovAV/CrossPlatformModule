using Colyseus.UI;
using Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    internal class GameRoot : MonoBehaviour
    {
        private const string GameSceneName = "Game";

        [SerializeField] private LobbyUI _lobbyUI;
        [SerializeField] private MultiplayerManager _multiplayerManager;

        private PlayerSettings _playerSettings;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _playerSettings = new PlayerSettings();

            _lobbyUI.Construct(_playerSettings);
        }

        private void OnEnable() =>
            SceneManager.sceneLoaded += OnSceneLoaded;

        private void OnDisable() =>
            SceneManager.sceneLoaded -= OnSceneLoaded;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case GameSceneName:
                    PrepareGameScene();
                    break;
            }
        }

        private void PrepareGameScene()
        {
            _multiplayerManager.FindGame(_playerSettings.Login);
            //if (_joinInfo.IsIdJoining)
            //    _multiplayerManager.FindGame(_playerSettings.Login, _joinInfo.JoinId);
            //else
            //    _multiplayerManager.FindGame(_playerSettings.Login);

            //_checkersRoot = FindObjectOfType<CheckersRoot>();
            //_checkersRoot.Construct(_multiplayerManager, _playerSettings);

            //_joinInfo.CleanInfo();
        }
    }
}