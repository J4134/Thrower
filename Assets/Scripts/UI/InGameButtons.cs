using Jaba.Thrower.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jaba.Thrower.UI
{
    public class InGameButtons : MonoBehaviour
    {
        public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        public void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1f;
        }

        public void Pause()
        {
            SceneEventBroker.CallPause();
            Time.timeScale = 0f;
        }

        public void Unpause()
        {
            Time.timeScale = 1f;
            SceneEventBroker.CallUnpause();
        }
    }
}