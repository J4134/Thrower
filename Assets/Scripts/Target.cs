using Jaba.Thrower.Helpers;
using System.Collections;
using UnityEngine;

namespace Jaba.Thrower
{
    public class Target : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _delay = 1f;

        #endregion

        #region BuiltIn Methods

        #region Subscribe/Unsubscribe

        private void OnEnable()
        {
            SceneEventBroker.OnTargetHitted += DestroyTarget;
            SceneEventBroker.OnCleanTargetHitted += DestroyTarget;
        }

        private void OnDisable()
        {
            SceneEventBroker.OnTargetHitted -= DestroyTarget;
            SceneEventBroker.OnCleanTargetHitted -= DestroyTarget;
        }

        #endregion

        #endregion

        #region Custom Methods

        private void DestroyTarget() => StartCoroutine(CallDestroyWithDelay(_delay));

        private IEnumerator CallDestroyWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneEventBroker.CallTargetDestroy();
            Destroy(gameObject);
        }

        #endregion
    }
}