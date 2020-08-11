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
    private Camera _cam;

    private TrajectoryRenderer _trajectoryRenderer;

    [SerializeField] private Vector2 offset = new Vector2(0f, 5f);

    private Vector2 _playerPosition;
    private Vector2 _itemSocketPosition;
    private Vector2 _throwPointPosition;
    private Vector2 _mousePosition;

    // Переписать используя сеттер
    private Vector2 _throwVector;

    private WaitForSeconds startDelay = new WaitForSeconds(0f);
    private WaitForSeconds afterThrowDelay = new WaitForSeconds(0.5f);

    private bool isPressed;

    IThrowable<Vector2> _iThrowable;

    #endregion

    #region Startup

    private void Awake()
    {
        // TODO: реализовать определение используемого префаба мяча

        Transform _transform = gameObject.transform;

        _playerPosition = _transform.position;
        _itemSocketPosition = _transform.GetChild(0).position;
        _throwPointPosition = _transform.GetChild(1).position;
        _trajectoryRenderer = TrajectoryRenderer.GetComponent<TrajectoryRenderer>();
        _cam = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(SpawnItem(startDelay));
    }

    #endregion

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        _mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        _throwVector = _mousePosition - _playerPosition + offset;

        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isPressed)
            {
                _trajectoryRenderer.DeleteTrajectory();

                if (_itemRB != null && _itemRB.isKinematic)
                {
                    _itemRB.isKinematic = false;

                    ThrowItem(_throwVector);
                }
            }

            isPressed = false;
        }

        if (isPressed)
        {
            _trajectoryRenderer.DrawTrajectory(_throwPointPosition, _throwVector);
        }
    }

    #region Item Controllers

    private void ChangeItemPos(Vector2 newPos)
    {
        _item.transform.position = newPos;
    }

    private void ThrowItem(Vector2 throwVector)
    {
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
