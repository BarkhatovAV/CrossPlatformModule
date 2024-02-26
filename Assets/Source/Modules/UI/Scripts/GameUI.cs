using Synchronization;
using UnityEngine;
using UnityEngine.UI;

namespace Colyseus.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private SceneSynchronizator _sceneSynchronizator;
        [SerializeField] private Button _squareSpawnButton;
        [SerializeField] private Button _circleSpawnButton;
        [SerializeField] private Button _saveDataButton;
        [SerializeField] private Button _sendDataButton;

        private void OnEnable()
        {
            _squareSpawnButton.onClick.AddListener(OnSquareSpawnButtonClick);
            _circleSpawnButton.onClick.AddListener(OnCircleSpawnButtonClick);
            _saveDataButton.onClick.AddListener(OnSaveDataButtonClick);
            _sendDataButton.onClick.AddListener(OnSendDataButtonClick);
        }

        private void OnDisable()
        {
            _squareSpawnButton.onClick.RemoveListener(OnSquareSpawnButtonClick);
            _circleSpawnButton.onClick.RemoveListener(OnCircleSpawnButtonClick);
            _saveDataButton.onClick.RemoveListener(OnSaveDataButtonClick);
            _sendDataButton.onClick.RemoveListener(OnSendDataButtonClick);
        }

        private void OnSquareSpawnButtonClick() =>
            _sceneSynchronizator.SetNewSquare();

        private void OnCircleSpawnButtonClick() =>
            _sceneSynchronizator.SetNewCircle();

        private void OnSaveDataButtonClick() =>
            _sceneSynchronizator.SaveSceneConfig();

        private void OnSendDataButtonClick() =>
            _sceneSynchronizator.SendSceneConfig();
    }
}