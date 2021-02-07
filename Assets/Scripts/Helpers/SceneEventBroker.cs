using System;

namespace Jaba.Thrower.Helpers
{
    public static class SceneEventBroker
    {
        #region Variables

        public static GameModes gameMode = GameModes.zen;

        public static event Action OnPaused;
        public static event Action OnUnpaused;
        public static event Action OnTargetHitted;
        public static event Action OnCleanTargetHitted;
        public static event Action OnTargetDestroyed;
        public static event Action OnMissed;
        public static event Action OnGameOver;

        #endregion

        #region Custom Methods

        public static void CallPause() => OnPaused?.Invoke();
        public static void CallUnpause() => OnUnpaused?.Invoke();
        public static void CallTargetHit() => OnTargetHitted?.Invoke();
        public static void CallCleanTargetHit() => OnCleanTargetHitted?.Invoke();
        public static void CallTargetDestroy() => OnTargetDestroyed?.Invoke();
        public static void CallGameOver() => OnGameOver?.Invoke();

        public static void CallMissed()
        {
            if (gameMode != GameModes.zen)
                CallGameOver();

            OnMissed?.Invoke();
        }

        #endregion
    }

    public enum GameModes
    {
        zen = 0,
        challange = 1
    }
}




