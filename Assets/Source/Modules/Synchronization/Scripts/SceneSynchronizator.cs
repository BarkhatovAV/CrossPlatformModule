using ConstantValues;
using Multiplayer;
using Save;
using System.Collections.Generic;
using UnityEngine;

namespace Synchronization
{
    public class SceneSynchronizator : MonoBehaviour
    {
        [SerializeField] private GameObject _squarePrefab;
        [SerializeField] private GameObject _circlePrefab;

        private List<SynchronizableObject> _synchronizableObjects = new List<SynchronizableObject>();
        private MultiplayerManager _multiplayerManager;
        private SceneConfig _sceneConfig;

        private void Awake() =>
            SynchronizeScene();

        private void OnDestroy()
        {
            if (_multiplayerManager != null)
                _multiplayerManager.SceneSynchronized -= SynchronizeScene;
        }

        public void SetMultiplayerManager(MultiplayerManager multiplayerManager)
        {
            _multiplayerManager = multiplayerManager;
            _multiplayerManager.SceneSynchronized += SynchronizeScene;
        }

        public void SaveSceneConfig()
        {
            UpdateInfo();
            Saver.Save(PlayerPrefsNames.SceneConfig, _sceneConfig);
        }

        public void SendSceneConfig() =>
            _multiplayerManager.TrySendMessage(MessagesNames.Synchronize, JsonUtility.ToJson(_sceneConfig));

        public void SetNewCircle() =>
            SetNewObject(PrefabsVariants.Circle);

        public void SetNewSquare() =>
            SetNewObject(PrefabsVariants.Square);

        private void SynchronizeScene()
        {
            _sceneConfig = Saver.Load<SceneConfig>(PlayerPrefsNames.SceneConfig);
            SetObjects(_sceneConfig);
        }

        private void SynchronizeScene(string jsonSceneConfig)
        {
            ClearScene();

            _sceneConfig = Saver.Unpack<SceneConfig>(jsonSceneConfig);
            SetObjects(_sceneConfig);
        }

        private void ClearScene()
        {
            for (int i = 0; i < _synchronizableObjects.Count; i++)
                Destroy(_synchronizableObjects[i].gameObject);

            _synchronizableObjects.Clear();
        }

        private void SetObjects(SceneConfig sceneConfig)
        {
            List<PositionProfile> positionProfiles = sceneConfig.profiles;

            for (int i = 0; i < positionProfiles.Count; i++)
                SetGettingObject(positionProfiles[i]);
        }

        private void SetGettingObject(PositionProfile positionProfile)
        {
            GameObject prefab = null;

            switch (positionProfile.PrefabVariant)
            {
                case PrefabsVariants.Square:
                    prefab = _squarePrefab;
                    break;
                case PrefabsVariants.Circle:
                    prefab = _circlePrefab;
                    break;
                default:
                    Debug.LogError("An attempt to use non-existant prefab");
                    break;
            }

            if (prefab != null)
            {
                GameObject figure = Instantiate(prefab, Vector3.zero, Quaternion.identity);

                if (figure.TryGetComponent<SynchronizableObject>(out SynchronizableObject synchronizableObject))
                {
                    synchronizableObject.Construct(positionProfile);
                    _synchronizableObjects.Add(synchronizableObject);
                }
            }
        }

        private void SetNewObject(PrefabsVariants prefabVariant)
        {
            GameObject prefab = null;

            switch (prefabVariant)
            {
                case PrefabsVariants.Square:
                    prefab = _squarePrefab;
                    break;
                case PrefabsVariants.Circle:
                    prefab = _circlePrefab;
                    break;
                default:
                    Debug.LogError("An attempt to use non-existant prefab");
                    break;
            }

            if (prefab != null)
            {
                PositionProfile positionProfile = CreatePositionProfile(prefabVariant);
                GameObject figure = Instantiate(prefab, Vector3.zero, Quaternion.identity);

                if (figure.TryGetComponent<SynchronizableObject>(out SynchronizableObject synchronizableObject))
                {
                    synchronizableObject.Construct(positionProfile);
                    _synchronizableObjects.Add(synchronizableObject);
                }
            }
        }

        private PositionProfile CreatePositionProfile(PrefabsVariants prefabVariant)
        {
            PositionProfile positionProfile = new PositionProfile();

            positionProfile.PrefabVariant = prefabVariant;
            positionProfile.xPos = 0f;
            positionProfile.yPos = 0f;
            positionProfile.xAng = 0f;
            positionProfile.yAng = 0f;
            positionProfile.xVel = 0f;
            positionProfile.yVel = 0f;
            positionProfile.AngVel = 0f;

            _sceneConfig.profiles.Add(positionProfile);

            return positionProfile;
        }

        private void UpdateInfo()
        {
            _sceneConfig.profiles.Clear();

            PositionProfile positionProfile;

            for (int i = 0; i < _synchronizableObjects.Count; i++)
            {
                positionProfile = _synchronizableObjects[i].GetCurrentPositionProfile();
                _sceneConfig.profiles.Add(positionProfile);
            }
        }
    }
}