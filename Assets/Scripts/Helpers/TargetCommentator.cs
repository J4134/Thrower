using UnityEngine;

namespace Jaba.Thrower.Helpers
{
    public class TargetCommentator : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        private string _onCleanTargetHitComment;

        [SerializeField]
        private string _onTargetHitComment;

        [SerializeField]
        private string _onMissComment;

        [SerializeField]
        private GameObject _commentPrefab;

        #endregion

        #region BuiltIn Methods

        #region Subscribe/Unsubscribe

        private void OnEnable()
        {
            SceneEventBroker.OnTargetHitted += CommentOnTargetHit;
            SceneEventBroker.OnCleanTargetHitted += CommentOnCleanTargetHit;
            SceneEventBroker.OnMissed += CommentOnMiss;
        }

        private void OnDisable()
        {
            SceneEventBroker.OnTargetHitted -= CommentOnTargetHit;
            SceneEventBroker.OnCleanTargetHitted -= CommentOnCleanTargetHit;
            SceneEventBroker.OnMissed -= CommentOnMiss;
        }

        #endregion

        #endregion

        #region Custom Methods

        private void SpawnLabel(string text)
        {
            var newComment = Instantiate(_commentPrefab, transform.position, Quaternion.identity);
            Destroy(newComment, 1f);
            newComment.GetComponent<TextMesh>().text = text;
        }

        private void CommentOnTargetHit() => SpawnLabel(_onTargetHitComment);
        private void CommentOnCleanTargetHit() => SpawnLabel(_onCleanTargetHitComment);
        private void CommentOnMiss() => SpawnLabel(_onMissComment);

        #endregion
    }
}