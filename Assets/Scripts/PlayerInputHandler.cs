using UnityEngine;
using System;

public class PlayerInputHandler : MonoBehaviour
{

    #region Field Declarations

    [SerializeField] 
    private Vector2 offset = new Vector2(0f, 0f);

    public event Action<Vector2> OnThrow;
    public event Action<Vector2> OnPress;

    private Camera _camera;

    private bool _isPressed;
    private bool _isActive = true;
    private Vector2 _mousePosition { get => _camera.ScreenToWorldPoint(Input.mousePosition); }

    #endregion

    #region Startup

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        SceneEventBroker.OnGameOver += DisableInput;
    }

    private void OnDisable()
    {
        SceneEventBroker.OnGameOver -= DisableInput;
    }

    #endregion

    private void DisableInput() => _isActive = false;

    #region Handle Input

    private void Update()
    {
        if (_isActive)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isPressed = true;
        }
         
        if (_isPressed)
        {
            OnPress?.Invoke(_mousePosition + offset);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_isPressed)
            {
                OnThrow?.Invoke(_mousePosition + offset);
            }

            _isPressed = false;
        }
    }

    #endregion
}
