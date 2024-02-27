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
            _positionProfile.xPos = _transform.position.x;
            _positionProfile.yPos = _transform.position.y;
            _positionProfile.xAng = _transform.eulerAngles.x;
            _positionProfile.yAng = _transform.eulerAngles.y;
            _positionProfile.xVel = _rigidbody2D.velocity.x;
            _positionProfile.yVel = _rigidbody2D.velocity.y;
            _positionProfile.AngVel = _rigidbody2D.angularVelocity;

            return _positionProfile;
        }

        private void SetCurrentPosition(PositionProfile positionProfile)
        {
            _positionProfile.PrefabVariant = positionProfile.PrefabVariant;

            Vector2 tempVector;

            tempVector.x = positionProfile.xPos;
            tempVector.y = positionProfile.yPos;
            _transform.position = tempVector;

            tempVector.x = positionProfile.xAng;
            tempVector.y = positionProfile.yAng;
            _transform.eulerAngles = tempVector;

            tempVector.x = positionProfile.xVel;
            tempVector.y = positionProfile.yVel;
            _rigidbody2D.velocity = tempVector;

            _rigidbody2D.angularVelocity = positionProfile.AngVel;
        }
    }
}