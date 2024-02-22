using UnityEngine;

namespace DragAndDrop
{
    [RequireComponent(typeof(Camera))]
    internal class Dragger : MonoBehaviour
    {
        private Camera _camera;
        private Vector2 _cursorePosition;
        private Collider2D _targetObject;
        private DraggingObject _selectedObject;

        private void Awake() =>
            _camera = GetComponent<Camera>();

        private void Update()
        {
            if(Input.GetMouseButton(0))
                _cursorePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
                AttemptSelectObject();

            if (_selectedObject != null)
                _selectedObject.transform.position = _cursorePosition;

            if (Input.GetMouseButtonUp(0) && _selectedObject != null)
            {
                _selectedObject.Undrag();
                _selectedObject = null;
            }
        }

        private void AttemptSelectObject()
        {
            _targetObject = Physics2D.OverlapPoint(_cursorePosition);

            if (_targetObject != null && _targetObject.TryGetComponent<DraggingObject>(out DraggingObject draggingObject))
            {
                _selectedObject = draggingObject;
                _selectedObject.Drag();
            }
        }
    }
}