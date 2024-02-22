using Save;
using UnityEngine;

namespace SavableObjects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SynchronizableObject : MonoBehaviour
    {
        [field: SerializeField] public PrefabsVariants PrefabVariant { get; private set; }

        public string Key { get; private set; }

        private Transform _transform;
        private Rigidbody2D _rigidbody2D;
        private PositionProfile _positionProfile;

        private void Awake()
        {
            _transform = transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void SetKey(string key) =>
            Key = key;

        public PositionProfile GetCurrentPositionProfile()
        {
            _positionProfile = new PositionProfile();

            _positionProfile.PrefabVariant = PrefabVariant;
            _positionProfile.CurrentPosition = _transform.position;
            _positionProfile.CurrentAngular = _transform.eulerAngles;
            _positionProfile.CurrentVelocity = _rigidbody2D.velocity;
            _positionProfile.CurrentAngularVelocity = _rigidbody2D.angularVelocity;

            Saver.Save(Key, _positionProfile);

            return _positionProfile;
        }

        public void SetCurrentPosition(PositionProfile positionProfile)
        {
            _transform.position = positionProfile.CurrentPosition;
            _transform.eulerAngles = positionProfile.CurrentAngular;
            _rigidbody2D.velocity = positionProfile.CurrentVelocity;
            _rigidbody2D.angularVelocity = positionProfile.CurrentAngularVelocity;

            Saver.Save(Key, _positionProfile);
        }
    }
}