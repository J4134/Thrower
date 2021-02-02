using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private GameObject _targetPrefab;
    private Transform _spawnZone;

    private float _leftEdgeX;
    private float _rightEdgeX;
    private float _topEdgeY;
    private float _bottomEdgeY;

    private void Awake()
    {
        Vector2 _spawnZonePosition = new Vector2();

        _spawnZone = GetComponentInChildren<Transform>();

        _spawnZonePosition =  _spawnZone.localPosition;
        _leftEdgeX = _spawnZonePosition.x + (_spawnZone.localScale.x / 2);
        _rightEdgeX = _spawnZonePosition.x - (_spawnZone.localScale.x / 2);
        _topEdgeY = _spawnZonePosition.y + (_spawnZone.localScale.y / 2);
        _bottomEdgeY = _spawnZonePosition.y - (_spawnZone.localScale.y / 2);
    }

    private void Start()
    {
        SpawnTarget();
    }

    private void OnEnable()
    {
        SceneEventBroker.OnTargetDestroyed += SpawnTarget;
    }

    private void OnDisable()
    {
        SceneEventBroker.OnTargetDestroyed -= SpawnTarget;
    }

    private Vector2 RandomPosition()
    {
        Vector2 newPosition = new Vector2(Random.Range(_leftEdgeX, _rightEdgeX), Random.Range(_topEdgeY, _bottomEdgeY));

        return newPosition;
    }

    private void SpawnTarget() => Instantiate(_targetPrefab, RandomPosition(), Quaternion.identity);
}
