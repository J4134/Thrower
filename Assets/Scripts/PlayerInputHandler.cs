using UnityEngine;
using System;
using Jaba.Thrower.Helpers;

namespace Jaba.Thrower
{
    public class PlayerInputHandler : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        private Vector2 offset = new Vector2(0f, 0f);

        public Camera _camera;

        public event Action<Vector2> OnThrow;
        public event Action<Vector2> OnPress;

        private bool _isPressed;
        private bool _isActive = true;

        private Vector2 _mousePosition { get => _camera.ScreenToWorldPoint(Input.mousePosition); }

        #endregion

        #region BuiltIn Methods

        private void Awake()
        {
            _camera = Camera.main;
        }

        #region Subscribe/Unsubscribe

        private void OnEnable()
        {
            SceneEventBroker.OnGameOver += DisableInput;
            SceneEventBroker.OnPaused += DisableInput;
            SceneEventBroker.OnUnpaused += EnableInput;
        }

        private void OnDisable()
        {
            SceneEventBroker.OnGameOver -= DisableInput;
            SceneEventBroker.OnPaused -= DisableInput;
            SceneEventBroker.OnUnpaused -= EnableInput;
        }

        #endregion

        #endregion

        #region Custom Methods

        private void DisableInput() => _isActive = false;

        private void EnableInput()
        {
            _isPressed = false;
            _isActive = true;
        }

        #region Handle Input

        private void Update()
        {
            if (_isActive)
                HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
                _isPressed = true;

            if (_isPressed)
                OnPress?.Invoke(_mousePosition + offset);

            if (Input.GetMouseButtonUp(0))
            {
                if (_isPressed)
                    OnThrow?.Invoke(_mousePosition + offset);

                _isPressed = false;
            }
        }

        #endregion

        #endregion
    }
}