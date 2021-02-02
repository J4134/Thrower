using System;

public enum GameModes
{
    zen = 0,
    challange =1 
}

public class SceneEventBroker
{
    public static GameModes gameMode = GameModes.zen; 

    public static event Action OnTargetHitted;
    public static event Action OnTargetDestroyed;
    public static event Action OnMissed;
    public static event Action OnGameOver;

    public static void CallTargetHit() => OnTargetHitted?.Invoke();
    public static void CallTargetDestroy() => OnTargetDestroyed?.Invoke();
    public static void CallGameOver() => OnGameOver?.Invoke();

    public static void CallMissed()
    {
        if (gameMode == GameModes.zen)
        {
            OnMissed?.Invoke();
        }
        else if (gameMode == GameModes.challange)
        {
            OnMissed?.Invoke();
            CallGameOver();
        }
    }


}
