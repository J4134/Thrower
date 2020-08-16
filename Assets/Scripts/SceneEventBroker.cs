using System;

public class SceneEventBroker
{
    public static event Action HitTarget;

    public static void CallHitTarget() => HitTarget?.Invoke();
}
