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

        public void SaveSceneConfig() =>
            Saver.Save(PlayerPrefsNames.SceneConfig, _sceneConfig);

        public void SendSceneConfig()
        {
            _multiplayerManager.TrySendMessage(MessagesNames.Synchronize, JsonUtility.ToJson(_sceneConfig));
            Debug.Log(JsonUtility.ToJson(_sceneConfig));
        }

        public void SetNewCircle() =>
            SetNewObject(PrefabsVariants.Circle);

        public void SetNewSquare() =>
            SetNewObject(PrefabsVariants.Square);

        private void SynchronizeScene()
        {
            _sceneConfig = Saver.Load<SceneConfig>(PlayerPrefsNames.SceneConfig);
            SetObjects();
        }

        private void SynchronizeScene(string jsonSceneConfig)
        {
            Debug.Log(jsonSceneConfig);
            _sceneConfig = Saver.Unpack<SceneConfig>(jsonSceneConfig);

            SetObjects();
        }

        private void SetObjects()
        {
            List<PositionProfile> positionProfiles = _sceneConfig.profiles;

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
                Instantiate(prefab, Vector3.zero, Quaternion.identity);

                if (prefab.TryGetComponent<SynchronizableObject>(out SynchronizableObject synchronizableObject))
                    synchronizableObject.Construct(positionProfile);
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
                Instantiate(prefab, Vector3.zero, Quaternion.identity);

                if (prefab.TryGetComponent<SynchronizableObject>(out SynchronizableObject synchronizableObject))
                    synchronizableObject.Construct(positionProfile);
            }
        }

        private PositionProfile CreatePositionProfile(PrefabsVariants prefabVariant)
        {
            PositionProfile positionProfile = new PositionProfile();

            positionProfile.PrefabVariant = prefabVariant;
            positionProfile.CurrentXPosition = 0f;
            positionProfile.CurrentYPosition = 0f;
            positionProfile.CurrentXAngular = 0f;
            positionProfile.CurrentYAngular = 0f;
            positionProfile.CurrentXVelocity = 0f;
            positionProfile.CurrentYVelocity = 0f;
            positionProfile.CurrentAngularVelocity = 0f;

            _sceneConfig.profiles.Add(positionProfile);

            return positionProfile;
        }
    }
}