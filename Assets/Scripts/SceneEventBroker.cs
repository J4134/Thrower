using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventBroker : MonoBehaviour
{
    public static event Action OnTargetHitted;
    public static event Action OnTargetDestroyed;
    public static event Action OnMissed;

    public static void CallTargetHit() => OnTargetHitted?.Invoke();
    public static void CallTargetDestroy() => OnTargetDestroyed?.Invoke();
    public static void CallMissed() => OnMissed?.Invoke();
}
