using UnityEngine;

[AddComponentMenu("Item/ItemScripts")]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour, IThrowable<Vector2>
{
    #region Field Declarations



    #endregion

    // Ссылка на RigidBody не кэшируется, т.к. используется один раз
    public void Throw(Vector2 throwVector)
    {
        GetComponent<Rigidbody2D>().AddForce(throwVector, ForceMode2D.Impulse);
        Destroy(gameObject, 7f);
    }

    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            // TODO: оповещение объекта score о пападании
            Debug.Log("Есть пробитие!");
            EventBroker.CallHitTarget();
        }
    }

    #endregion
}
