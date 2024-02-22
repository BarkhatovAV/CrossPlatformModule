using Multiplayer;
using Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavableObjects
{
    public class ObjectsSynchronizator : MonoBehaviour
    {
        [SerializeField] private List<SynchronizableObject> _synchronizableObjects;
        [SerializeField] private GameObject _squarePrefab;
        [SerializeField] private GameObject _circlePrefab;

        private MultiplayerManager _multiplayerManager;

        private void Awake()
        {
            SetPositions();
        }

        private void OnDisable()
        {
            if (_multiplayerManager != null)
                _multiplayerManager.ObjectSynchronized -= SynchronizeObject;
        }

        public void SetMultiplayerManager(MultiplayerManager multiplayerManager)
        {
            _multiplayerManager = multiplayerManager;
            _multiplayerManager.ObjectSynchronized += SynchronizeObject;
        }

        public void SendInfo()
        {
            PositionProfile positionProfile;

            for (int i = 0; i < _synchronizableObjects.Count; i++)
            {
                positionProfile = _synchronizableObjects[i].GetCurrentPositionProfile();

                _multiplayerManager.TrySendMessage(MessagesNames.Synchronize, positionProfile);
            }
        }

        public void SynchronizeObject(string jsomPositionData)
        {
            PositionProfile positionProfile = Saver.Unpack<PositionProfile>(jsomPositionData);

            SetGettingObject(positionProfile);
        }

        public void SendObjectsInfo()
        {
            for(int i = 0; i < _synchronizableObjects.Count; i++)
            {
                //Saver.
            }
        }

        public void SetNewObject()
        {

        }

        public void SetGettingObject(PositionProfile positionProfile)
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
            
            if(prefab != null)
            {
                Instantiate(prefab, Vector3.zero, Quaternion.identity);

                if (prefab.TryGetComponent<SynchronizableObject>(out SynchronizableObject synchronizableObject))
                    synchronizableObject.SetCurrentPosition(positionProfile);
            }
        }

        private void SetPositions()
        {
            PositionProfile positionProfile;
            string jsonDataString;
            string key;

            for (int i = 0; i < _synchronizableObjects.Count; i++)
            {
                key = i.ToString();
                _synchronizableObjects[i].SetKey(key);

                if (PlayerPrefs.HasKey(key))
                {
                    jsonDataString = PlayerPrefs.GetString(key);
                    positionProfile = Saver.Unpack<PositionProfile>(jsonDataString);

                    _synchronizableObjects[i].SetCurrentPosition(positionProfile);  
                }
            }
        }
    }
}