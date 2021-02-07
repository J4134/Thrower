using UnityEngine;

namespace Jaba.Thrower.Helpers
{
    public class TriggerType : MonoBehaviour
    {
        public enum Type { targetFirst, targetSecond, miss };

        public Type type = Type.targetFirst;
    }
}