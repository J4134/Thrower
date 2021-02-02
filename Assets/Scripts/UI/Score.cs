using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    #region Varibles

    [SerializeField] private int _scoreCount = -1;
    [SerializeField] private string _prefix = "Score";

    private Text scoreLabel;

    #endregion

    #region BuiltIn Methods

    private void OnEnable()
    {
        SceneEventBroker.OnTargetHitted += UpdateScore;
    }

    private void OnDisable()
    {
        SceneEventBroker.OnTargetHitted -= UpdateScore;
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
        _scoreCount++;
        scoreLabel.text = $"{_prefix}: {Convert.ToString(_scoreCount)}";
    }

    #endregion

}
