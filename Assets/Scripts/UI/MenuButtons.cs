using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void LoadZenMode()
    {
        SceneEventBroker.gameMode = GameModes.zen;
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadChallengeMode()
    {
        SceneEventBroker.gameMode = GameModes.challange;
        SceneManager.LoadScene("SampleScene");
    }
}
