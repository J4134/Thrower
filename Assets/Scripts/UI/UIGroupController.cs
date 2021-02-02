using UnityEngine;

public class UIGroupController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private CanvasGroup inGameGroup;

    [SerializeField]
    private CanvasGroup onGameOverGroup;

    #endregion

    #region BuiltIn Methods

    private void OnEnable()
    {
        SceneEventBroker.OnGameOver += OnGameOverUI;
    }

    private void OnDisable()
    {
        SceneEventBroker.OnGameOver -= OnGameOverUI;
    }

    private void Start()
    {
        ShowGroup(inGameGroup);
        HideGroup(onGameOverGroup);
    }

    #endregion

    #region Custom Methods

    public void ShowGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1;
    }

    public void HideGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;
    }

    private void OnGameOverUI()
    {
        ShowGroup(onGameOverGroup);
        HideGroup(inGameGroup);
    }

    #endregion
}
