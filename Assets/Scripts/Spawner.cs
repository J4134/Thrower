using Jaba.Thrower.Helpers;
using UnityEngine;

namespace Jaba.Thrower
{
    public class Spawner : MonoBehaviour
    {
        #region Varibles

        [SerializeField]
        private GameObject _targetPrefab;

        [SerializeField]
        Vector2 _spawnZonePosition = new Vector2();

        private Transform _spawnZone;

        private float _leftEdgeX;
        private float _rightEdgeX;
        private float _topEdgeY;
        private float _bottomEdgeY;

        #endregion

        #region BuiltIn Methods

        #region Subscribe/Unsubscribe

        private void OnEnable()
        {
            SceneEventBroker.OnTargetDestroyed += SpawnTarget;
        }

        private void OnDisable()
        {
            SceneEventBroker.OnTargetDestroyed -= SpawnTarget;
        }

        #endregion

        private void Awake()
        {
            _spawnZone = GetComponentInChildren<Transform>();

            _spawnZonePosition = _spawnZone.position;

            _leftEdgeX = _spawnZonePosition.x + _spawnZone.localScale.x / 2;
            _rightEdgeX = _spawnZonePosition.x - _spawnZone.localScale.x / 2;
            _topEdgeY = _spawnZonePosition.y + _spawnZone.localScale.y / 2;
            _bottomEdgeY = _spawnZonePosition.y - _spawnZone.localScale.y / 2;
        }

        private void Start()
        {
            SpawnTarget();
        }

        #endregion

        #region Custom Methods

        private Vector2 RandomPosition() => new Vector2(Random.Range(_leftEdgeX, _rightEdgeX), Random.Range(_topEdgeY, _bottomEdgeY));

        private void SpawnTarget() => Instantiate(_targetPrefab, RandomPosition(), Quaternion.identity);

        #endregion
    }
}