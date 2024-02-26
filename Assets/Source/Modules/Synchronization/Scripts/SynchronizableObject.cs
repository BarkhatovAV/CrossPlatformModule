using Save;
using UnityEngine;

namespace Synchronization
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SynchronizableObject : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody2D _rigidbody2D;
        private PositionProfile _positionProfile;

        public void Construct(PositionProfile positionProfile)
        {
            _transform = transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _positionProfile = positionProfile;
            SetCurrentPosition(positionProfile);
        }

        public PositionProfile GetCurrentPositionProfile()
        {
            _positionProfile.CurrentPosition = _transform.position;
            _positionProfile.CurrentAngular = _transform.eulerAngles;
            _positionProfile.CurrentVelocity = _rigidbody2D.velocity;
            _positionProfile.CurrentAngularVelocity = _rigidbody2D.angularVelocity;

            return _positionProfile;
        }

        private void SetCurrentPosition(PositionProfile positionProfile)
        {
            _positionProfile.PrefabVariant = positionProfile.PrefabVariant;
            _transform.position = positionProfile.CurrentPosition;
            _transform.eulerAngles = positionProfile.CurrentAngular;
            _rigidbody2D.velocity = positionProfile.CurrentVelocity;
            _rigidbody2D.angularVelocity = positionProfile.CurrentAngularVelocity;
        }
    }
}