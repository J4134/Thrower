using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Field Declarations

    [SerializeField] private GameObject itemPrefab;

    // TODO: разобраться с получением ссылки на trajectory renderer и itemPrefab (выбор скина)
    // переписать обращение к рендеру траектории через обсервер
    public GameObject TrajectoryRenderer;

    private GameObject _item;
    private Rigidbody2D _itemRB;

    private TrajectoryRenderer _trajectoryRenderer;
    private PlayerInputHandler _playerInput;

    [SerializeField] private Vector2 _offset = new Vector2(0f, 5f);

    private Vector2 _playerPosition;
    private Vector2 _itemSocketPosition;
    private Vector2 _throwPointPosition;

    private Vector2 _throwVector; 

    private WaitForSeconds startDelay = new WaitForSeconds(0f);
    private WaitForSeconds afterThrowDelay = new WaitForSeconds(0.5f);

    private bool isPressed;

    #endregion

    private void RecalculateThrowVector(Vector2 mousePosition, Vector2 playerPosition, Vector2 offset) => _throwVector = mousePosition - playerPosition + offset;

    #region Startup

    private void OnEnable()
    {
        _playerInput.OnPress += OnPressHandler;
        _playerInput.OnThrow += OnThrowHandler;
    }

    private void OnThrowHandler(Vector2 mousePosition)
    {
        _trajectoryRenderer.DeleteTrajectory();

        RecalculateThrowVector(mousePosition, _playerPosition, _offset);

        if (_itemRB != null && _itemRB.isKinematic)
        {
            ThrowItem(_throwVector);
        }
    }

    private void OnPressHandler(Vector2 mousePosition)
    {
        RecalculateThrowVector(mousePosition, _playerPosition, _offset);

        _trajectoryRenderer.DrawTrajectory(_throwPointPosition, _throwVector);
    }

    private void Awake()
    {
        // TODO: реализовать определение используемого префаба мяча

        Transform _transform = gameObject.transform;

        _playerPosition = _transform.position;
        _itemSocketPosition = _transform.GetChild(0).position;
        _throwPointPosition = _transform.GetChild(1).position;
        _trajectoryRenderer = TrajectoryRenderer.GetComponent<TrajectoryRenderer>();
        _playerInput = GetComponent<PlayerInputHandler>();
    }

    private void Start()
    {
        StartCoroutine(SpawnItem(startDelay));
    }

    private void OnDisable()
    {
        _playerInput.OnPress -= OnPressHandler;
        _playerInput.OnThrow -= OnThrowHandler;
    }

    #endregion

    #region Item Controllers

    private void ChangeItemPos(Vector2 newPos)
    {
        _item.transform.position = newPos;
    }

    private void ThrowItem(Vector2 throwVector)
    {
        _itemRB.isKinematic = false;

        ChangeItemPos(_throwPointPosition);

        _item.GetComponent<IThrowable<Vector2>>()?.Throw(throwVector);

        StartCoroutine(SpawnItem(afterThrowDelay));
    }

    private IEnumerator SpawnItem(WaitForSeconds delay)
    {
        yield return delay;
        _item = Instantiate(itemPrefab, _itemSocketPosition, Quaternion.identity);
        _itemRB = _item.GetComponent<Rigidbody2D>();
    }

    #endregion
}
