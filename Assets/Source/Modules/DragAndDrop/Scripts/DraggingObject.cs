using UnityEngine;

namespace DragAndDrop
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    internal class DraggingObject : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake() =>
            _rigidbody2D = GetComponent<Rigidbody2D>();

        internal void Drag()
        {
            _rigidbody2D.velocity = Vector3.zero;
            _rigidbody2D.angularVelocity = 0;
            _rigidbody2D.gravityScale = 0;
        }

        internal void Undrag() =>
            _rigidbody2D.gravityScale = 1;
    }
}