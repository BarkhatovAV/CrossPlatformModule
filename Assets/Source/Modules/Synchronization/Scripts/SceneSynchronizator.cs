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
        //private SceneConfig _sceneConfig;

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
            _multiplayerManager.Test += Show;
        }

        public void SynchronizeScene(string jsonSceneConfig)
        {
            //_sceneConfig = Saver.Unpack<SceneConfig>(jsonSceneConfig);

            SetObjects();
        }

        private void Show(string jsonTestData)
        {
            Test test = JsonUtility.FromJson<Test>(jsonTestData);
            Debug.Log(test.testIndex);
        }

        public void SaveSceneConfig()
        {
            //Saver.Save(PlayerPrefsNames.SceneConfig, _sceneConfig);
        }

        public void SendSceneConfig()
        {
            //string sceneConfigJson = JsonUtility.ToJson(_sceneConfig);
            //_multiplayerManager.TrySendMessage(MessagesNames.Synchronize, _sceneConfig);
        }

        public void SetNewCircle()
        {
            Test test = new Test();
            test.testIndex = 100;

            _multiplayerManager.TrySendMessage(MessagesNames.Test, test);

            SetNewObject(PrefabsVariants.Circle);
        }

        public void SetNewSquare() =>
            SetNewObject(PrefabsVariants.Square);

        private void SynchronizeScene()
        {
            PlayerPrefs.DeleteAll();

            if (PlayerPrefs.HasKey(PlayerPrefsNames.SceneConfig))
            {
                string sceneConfigJson = PlayerPrefs.GetString(PlayerPrefsNames.SceneConfig);
                //_sceneConfig = Saver.Load<SceneConfig>(sceneConfigJson);
                Debug.Log("Я получил SceneConfig");
            }
            else
            {
                //_sceneConfig = new SceneConfig();

                ////_sceneConfig.prefabVariants = new List<PrefabsVariants>();
                //_sceneConfig.prefabVariants = new List<int>();
                //_sceneConfig.positions = new List<Vector2>();
                //_sceneConfig.angulars = new List<Vector2>();
                //_sceneConfig.velocities = new List<Vector2>();
                //_sceneConfig.angularVelocities = new List<float>();

                //Saver.Save(PlayerPrefsNames.SceneConfig, _sceneConfig);
            }

            SetObjects();
        }

        private void SetObjects()
        {
            PositionProfile positionProfile;

            //for (int i = 0; i < _sceneConfig.positions.Count; i++)
            //{
            //    positionProfile = new PositionProfile();

            //    positionProfile.PrefabVariant = _sceneConfig.prefabVariants[i];
            //    positionProfile.CurrentPosition = _sceneConfig.positions[i];
            //    positionProfile.CurrentAngular = _sceneConfig.angulars[i];
            //    positionProfile.CurrentVelocity = _sceneConfig.velocities[i];
            //    positionProfile.CurrentAngularVelocity = _sceneConfig.angularVelocities[i];

            //    SetGettingObject(positionProfile);
            //}
        }

        private void SetGettingObject(PositionProfile positionProfile)
        {
            //GameObject prefab = null;

            //switch (positionProfile.PrefabVariant)
            //{
            //    case PrefabsVariants.Square:
            //        prefab = _squarePrefab;
            //        break;
            //    case PrefabsVariants.Circle:
            //        prefab = _circlePrefab;
            //        break;
            //    default:
            //        Debug.LogError("An attempt to use non-existant prefab");
            //        break;
            //}
            GameObject prefab = _circlePrefab;

            if (prefab != null)
            {
                Instantiate(prefab, Vector3.zero, Quaternion.identity);

                if (prefab.TryGetComponent<SynchronizableObject>(out SynchronizableObject synchronizableObject))
                    synchronizableObject.Construct(positionProfile);
            }
        }

        private void SetNewObject(PrefabsVariants prefabVariant)
        {
            //GameObject prefab = null;

            //switch (prefabVariant)
            //{
            //    case PrefabsVariants.Square:
            //        prefab = _squarePrefab;
            //        break;
            //    case PrefabsVariants.Circle:
            //        prefab = _circlePrefab;
            //        break;
            //    default:
            //        Debug.LogError("An attempt to use non-existant prefab");
            //        break;
            //}

            GameObject prefab = _circlePrefab;

            if (prefab != null)
            {
                PositionProfile positionProfile = CreatePositionProfile(0);
                Instantiate(prefab, Vector3.zero, Quaternion.identity);

                if (prefab.TryGetComponent<SynchronizableObject>(out SynchronizableObject synchronizableObject))
                    synchronizableObject.Construct(positionProfile);
            }
        }

        private PositionProfile CreatePositionProfile(int prefabVariant)
        {
            PositionProfile positionProfile = new PositionProfile();

            positionProfile.PrefabVariant = prefabVariant;
            positionProfile.CurrentPosition = Vector2.zero;
            positionProfile.CurrentAngular = Vector2.zero;
            positionProfile.CurrentVelocity = Vector2.zero;
            positionProfile.CurrentAngularVelocity = 0f;

            //_sceneConfig.Add(positionProfile);
            //Saver.Save(PlayerPrefsNames.SceneConfig, _sceneConfig);

            return positionProfile;
        }
    }
}