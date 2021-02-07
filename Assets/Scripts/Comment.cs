using UnityEngine;

namespace Jaba.Thrower
{
    [RequireComponent(typeof(TextMesh))]
    public class Comment : MonoBehaviour
    {
        #region BuiltIn Methods

        private void Start()
        {
            Destroy(gameObject, 1f);
        }

        private void Update()
        {
            transform.position += Vector3.up * Time.deltaTime;
        }

        #endregion
    }
}