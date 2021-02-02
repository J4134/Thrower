using UnityEngine;

public class TriggerType : MonoBehaviour
{
    public enum Type { targetFirst, targetSecond, miss };

    public Type type = Type.targetFirst;
}
