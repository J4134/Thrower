using UnityEngine;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    private Camera _camera;

    public event Action<Vector2> OnThrow;
    public event Action<Vector2> OnPress;

    private bool _isPressed;

    private Vector2 _mousePosition { get => _camera.ScreenToWorldPoint(Input.mousePosition); }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isPressed = true;
        }
         
        if (_isPressed)
        {
            OnPress?.Invoke(_mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_isPressed)
            {
                OnThrow?.Invoke(_mousePosition);
            }

            _isPressed = false;
        }
    }
}
