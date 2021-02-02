using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    #region Varibles

    public int scoreCount { get; private set; } = -1;
    [SerializeField] private string _prefix = "Score";

    private Text scoreLabel;

    #endregion

    #region BuiltIn Methods

    private void OnEnable()
    {
        SceneEventBroker.OnTargetHitted += UpdateScore;
        SceneEventBroker.OnGameOver += SaveScores;
    }

    private void OnDisable()
    {
        SceneEventBroker.OnTargetHitted -= UpdateScore;
        SceneEventBroker.OnGameOver -= SaveScores;
    }

    private void Awake()
    {
        scoreLabel = GetComponent<Text>();
    }

    private void Start()
    {
        UpdateScore();
    }

    #endregion

    #region Custom Methods

    private void UpdateScore()
    {
        scoreCount++;
        scoreLabel.text = $"{_prefix}: {Convert.ToString(scoreCount)}";
    }

    private void SaveScores() => ProgressKeeper.UpdateMaxScore(scoreCount);

    #endregion

}
