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
            _positionProfile.CurrentXPosition = _transform.position.x;
            _positionProfile.CurrentYPosition = _transform.position.y;
            _positionProfile.CurrentXAngular = _transform.eulerAngles.x;
            _positionProfile.CurrentYAngular = _transform.eulerAngles.y;
            _positionProfile.CurrentXVelocity = _rigidbody2D.velocity.x;
            _positionProfile.CurrentYVelocity = _rigidbody2D.velocity.y;
            _positionProfile.CurrentAngularVelocity = _rigidbody2D.angularVelocity;

            return _positionProfile;
        }

        private void SetCurrentPosition(PositionProfile positionProfile)
        {
            _positionProfile.PrefabVariant = positionProfile.PrefabVariant;

            Vector2 tempVector;

            tempVector.x = positionProfile.CurrentXPosition;
            tempVector.y = positionProfile.CurrentYPosition;
            _transform.position = tempVector;

            tempVector.x = positionProfile.CurrentXAngular;
            tempVector.y = positionProfile.CurrentYAngular;
            _transform.eulerAngles = tempVector;

            tempVector.x = positionProfile.CurrentXVelocity;
            tempVector.y = positionProfile.CurrentYVelocity;
            _rigidbody2D.velocity = tempVector;

            _rigidbody2D.angularVelocity = positionProfile.CurrentAngularVelocity;
        }
    }
}