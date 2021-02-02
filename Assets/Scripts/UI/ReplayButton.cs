using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
