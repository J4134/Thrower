using UnityEngine;
using UnityEngine.UI;

public class UIGroupController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject inGameGroup;

    [SerializeField]
    private GameObject onGameOverGroup;

    [SerializeField]
    private GameObject pauseGroup;

    [SerializeField]
    private Text gameOverLabel;

    [SerializeField]
    private Score score;

    #endregion

    #region BuiltIn Methods

    private void OnEnable()
    {
        SceneEventBroker.OnGameOver += OnGameOverUI;
        SceneEventBroker.OnPaused += OnPausedUI;
        SceneEventBroker.OnUnpaused += OnStartUI;
    }

    private void OnDisable()
    {
        SceneEventBroker.OnGameOver -= OnGameOverUI;
        SceneEventBroker.OnPaused -= OnPausedUI;
        SceneEventBroker.OnUnpaused -= OnStartUI;
    }

    private void Start()
    {
        OnStartUI();
    }

    #endregion

    #region Custom Methods

    private void OnGameOverUI()
    {
        RewriteGameOverText();
        onGameOverGroup.SetActive(true);
        inGameGroup.SetActive(false);
        pauseGroup.SetActive(false);
    }

    private void OnStartUI()
    {
        inGameGroup.SetActive(true);
        onGameOverGroup.SetActive(false);
        pauseGroup.SetActive(false);
    }

    private void OnPausedUI()
    {
        pauseGroup.SetActive(true);
        inGameGroup.SetActive(false);
        onGameOverGroup.SetActive(false);
    }

    private void RewriteGameOverText()
    {
        gameOverLabel.text = "You lose :(" + "\n\n" + "Score: " + score.scoreCount + "\n" + "Best score: " + PlayerPrefs.GetInt("maxScore"); 
    }

    #endregion
}
