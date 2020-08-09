using System;

public class EventBroker
{
    public static event Action HitTarget;

    public static void CallHitTarget()
    {
        HitTarget?.Invoke();
    }
}
