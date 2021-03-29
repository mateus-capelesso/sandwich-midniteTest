using System;
using UnityEngine;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        public float swipeThreshold = 50f;
        public float timeThreshold = 0.3f;

        [SerializeField]
        private Camera mainCamera;

        public static Action<Direction, GameObject> OnSwipeDetected;

        private Vector2 _fingerDown;
        private DateTime _fingerDownTime;
        private Vector2 _fingerUp;
        private DateTime _fingerUpTime;
        private GameObject _clickedObject;

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _fingerDown = Input.mousePosition;
                _fingerUp = Input.mousePosition;
                GetObjectsClicked(Input.mousePosition);
                _fingerDownTime = DateTime.Now;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _fingerDown = Input.mousePosition;
                _fingerUpTime = DateTime.Now;
                CheckSwipe();
            }

            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _fingerDown = touch.position;
                        _fingerUp = touch.position;
                        _fingerDownTime = DateTime.Now;
                        GetObjectsClicked(touch.position);
                        break;
                    case TouchPhase.Ended:
                        _fingerDown = touch.position;
                        _fingerUpTime = DateTime.Now;
                        CheckSwipe();
                        break;
                }
            }
        }

        private void CheckSwipe()
        {
            var duration = (float) _fingerUpTime.Subtract(_fingerDownTime).TotalSeconds;
            if (duration > timeThreshold) return;

            var deltaX = _fingerDown.x - _fingerUp.x;
            var deltaY = _fingerDown.y - _fingerUp.y;

            if (deltaX > deltaY)
            {
                if (Mathf.Abs(deltaX) > swipeThreshold)
                {
                    if (deltaX > 0)
                    {
                        OnSwipeDetected?.Invoke(Direction.Right, _clickedObject);
                    }
                    else if (deltaX < 0)
                    {
                        OnSwipeDetected?.Invoke(Direction.Left, _clickedObject);
                    }
                }
            }
            else
            {
                if (Mathf.Abs(deltaY) > swipeThreshold)
                {
                    if (deltaY > 0)
                    {
                        OnSwipeDetected?.Invoke(Direction.Top, _clickedObject);
                    }
                    else if (deltaY < 0)
                    {
                        OnSwipeDetected?.Invoke(Direction.Bottom, _clickedObject);
                    }
                }
            }
        
            _fingerUp = _fingerDown;
        }

        private void GetObjectsClicked(Vector3 position)
        {
            var ray = mainCamera.ScreenPointToRay(position);
            if( Physics.Raycast( ray, out var hit, 100 ) )
            {
                _clickedObject = hit .transform.gameObject;
            }
        }
    }
}