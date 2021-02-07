using Jaba.Thrower.Helpers;
using UnityEngine;

namespace Jaba.Thrower
{
    [AddComponentMenu("ItemScript")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour, IThrowable<Vector2>
    {
        #region Variables

        [SerializeField]
        private float _startToque = 120f;

        private Rigidbody2D _rigidbody;

        private int _collisionsCount = 0;

        private bool _canHitTarget = true;
        private bool _canMiss = true;

        private bool _isCalledMiss = false;
        private bool _isHittedFirstTarget = false;
        private bool _isHittedSecondTarget = false;

        #endregion

        #region BuiltIn Methods

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            // Чтобы мяч до броска не сталкивался с брошенными мячами
            gameObject.layer = 8;

            // Слой мяча после броска - 8
            Physics2D.IgnoreLayerCollision(8, 9);
        }

        private void OnDestroy()
        {
            if (!_isHittedSecondTarget && !_isCalledMiss)
            {
                SceneEventBroker.CallMissed();
            }
        }

        #region Collisions

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _collisionsCount++;
        }

        private void OnTriggerEnter2D(Collider2D trigger)
        {
            if (!trigger.GetComponent<TriggerType>())
            {
                return;
            }

            var triggerType = trigger.GetComponent<TriggerType>().type;

            if (triggerType == TriggerType.Type.targetFirst && _canHitTarget)
            {
                _isHittedFirstTarget = true;
            }
            else if (triggerType == TriggerType.Type.targetSecond && _canHitTarget)
            {
                if (_isHittedFirstTarget)
                {
                    _canHitTarget = false;
                    _canMiss = false;
                    _isHittedSecondTarget = true;

                    if (_collisionsCount > 0)
                        SceneEventBroker.CallTargetHit();
                    else
                        SceneEventBroker.CallCleanTargetHit();
                }
            }
            else
            {
                if (!_isHittedSecondTarget && _canMiss)
                {
                    SceneEventBroker.CallMissed();
                    _isCalledMiss = true;
                    _canMiss = false;
                }
            }
        }

        #endregion

        #endregion

        #region Custom Methods

        public void Throw(Vector2 throwVector)
        {
            _rigidbody.AddForce(throwVector, ForceMode2D.Impulse);
            _rigidbody.AddTorque(_startToque);
            gameObject.layer = 9;
            Destroy(gameObject, 7f);
        }

        #endregion
    }
}