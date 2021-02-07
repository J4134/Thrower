using UnityEngine;

namespace Jaba.Thrower.Helpers
{
    public static class ProgressKeeper
    {
        public static void UpdateMaxScore(int newMaxScore)
        {
            if (newMaxScore > PlayerPrefs.GetInt("maxScore"))
                PlayerPrefs.SetInt("maxScore", newMaxScore);
        }
    }
}