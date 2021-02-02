using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{

    #region Field Declarations

    [SerializeField] private GameObject _itemPrefab;

    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;

    [SerializeField] private Vector2 _offset = new Vector2(0f, 5f);

    private GameObject _item;
    private Rigidbody2D _itemRB; 
    private PlayerInputHandler _playerInput;

    private Vector2 _playerPosition;
    private Vector2 _itemSocketPosition;
    private Vector2 _throwPointPosition;
    private Vector2 _throwVector; 

    private WaitForSeconds _startDelay = new WaitForSeconds(0f);
    private WaitForSeconds _afterThrowDelay = new WaitForSeconds(0.5f);

    #endregion

    private void RecalculateThrowVector(Vector2 mousePosition, Vector2 playerPosition, Vector2 offset) => _throwVector = mousePosition - playerPosition + offset;

    #region Startup

    private void Awake()
    {
        Transform _transform = gameObject.transform;

        // TODO: переписать к хуям
        _playerPosition = _transform.position;
        _itemSocketPosition = _transform.GetChild(0).position;
        _throwPointPosition = _transform.GetChild(1).position;
        _playerInput = GetComponent<PlayerInputHandler>();
    }

    private void Start()
    {
        StartCoroutine(SpawnItem(_startDelay));
    }

    #endregion

    #region Subscribe / Unsubscribe

    private void OnEnable()
    {
        _playerInput.OnPress += OnPressHandler;
        _playerInput.OnThrow += OnThrowHandler;
    }

    private void OnDisable()
    {
        _playerInput.OnPress -= OnPressHandler;
        _playerInput.OnThrow -= OnThrowHandler;
    }

    #endregion

    #region Handlers

    private void OnThrowHandler(Vector2 mousePosition)
    {
        _trajectoryRenderer.DeleteTrajectory();

        RecalculateThrowVector(mousePosition, _playerPosition, _offset);

        ThrowItem(_throwVector);
    }

    private void OnPressHandler(Vector2 mousePosition)
    {
        RecalculateThrowVector(mousePosition, _playerPosition, _offset);

        _trajectoryRenderer.DrawTrajectory(_throwPointPosition, _throwVector);
    }

    #endregion

    #region Item Controllers

    private void ChangeItemPos(Vector2 newPos)
    {
        _item.transform.position = newPos;
    }

    private void ThrowItem(Vector2 throwVector)
    {
        if (_itemRB.isKinematic && _item != null)
        {
            _itemRB.isKinematic = false;

            ChangeItemPos(_throwPointPosition);

            _item.GetComponent<IThrowable<Vector2>>()?.Throw(throwVector);

            StartCoroutine(SpawnItem(_afterThrowDelay));
        }
       
    }

    private IEnumerator SpawnItem(WaitForSeconds delay)
    {
        yield return delay;
        _item = Instantiate(_itemPrefab, _itemSocketPosition, Quaternion.identity);
        _itemRB = _item.GetComponent<Rigidbody2D>();
    }

    #endregion
}
