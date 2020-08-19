using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField] private float _delay = 1f;

    private void OnEnable()
    {
        SceneEventBroker.OnTargetHitted += DestroyTarget;
    }

    private void OnDisable()
    {
        SceneEventBroker.OnTargetHitted -= DestroyTarget;
    }

    private void DestroyTarget()
    {
        StartCoroutine(CallDestroyWithDelay(_delay));
    }

    private IEnumerator CallDestroyWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneEventBroker.CallTargetDestroy();
        Destroy(gameObject);
    }
}
