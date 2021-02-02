using UnityEngine;

[AddComponentMenu("ItemScript")]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour, IThrowable<Vector2>
{
    #region Field Declarations

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private int _collisionsCount = 0;

    private bool _canHitTarget = true;

    private bool _isCalledMiss = false;
    private bool _isHittedFirstTarget = false;
    private bool _isHittedSecondTarget = false;

    #endregion

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
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

    public void Throw(Vector2 throwVector)
    {
       _rigidbody.AddForce(throwVector, ForceMode2D.Impulse);
        gameObject.layer = 9;
        Destroy(gameObject, 7f);
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
        else
        {
            var triggerType = trigger.GetComponent<TriggerType>().type;

            if ((triggerType == TriggerType.Type.targetFirst) && _canHitTarget)
            {
                _isHittedFirstTarget = true;
            }
            else if ((triggerType == TriggerType.Type.targetSecond) && _canHitTarget)
            {
                if (_isHittedFirstTarget)
                {
                    _canHitTarget = false;
                    SceneEventBroker.CallTargetHit();
                    _isHittedSecondTarget = true;

                    if (_collisionsCount > 0)
                        print("Обычное попадание");
                    else
                        print("Чистое попадание");
                }
            }
            else if (triggerType == TriggerType.Type.miss)
            {
                if (!_isHittedSecondTarget)
                {
                    SceneEventBroker.CallMissed();
                }
            }
        }
    }

    #endregion
}
