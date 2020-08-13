using UnityEngine;

[AddComponentMenu("ItemScript")]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour, IThrowable<Vector2>
{
    #region Field Declarations

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    #endregion


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(8, 9); // Слой мяча - 8
    }

    public void Throw(Vector2 throwVector)
    {
       _rigidbody.AddForce(throwVector, ForceMode2D.Impulse);
        Destroy(gameObject, 7f);
    }

    #region Collisions

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == 8)
        //{
        //    Physics.IgnoreCollision2D(_collider, 0);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            // TODO: оповещение объекта score о пападании
            Debug.Log("Есть пробитие!");
            SceneEventBroker.CallHitTarget();
        }
    }

    #endregion
}
