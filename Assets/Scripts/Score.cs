using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    [SerializeField] private int _scoreCount = -1;
    [SerializeField] private string _prefix = "Score";

    private Text scoreLabel;

    private void OnEnable()
    {
        SceneEventBroker.OnTargetHitted += UpdateScore;
    }

    private void Awake()
    {
        // TODO: реализовать перевод на старте

        scoreLabel = GetComponent<Text>();
    }

    private void Start()
    {
        UpdateScore();
    }

    private void OnDisable()
    {
        SceneEventBroker.OnTargetHitted -= UpdateScore;
    }

    private void UpdateScore()
    {
        _scoreCount++;
        scoreLabel.text = $"{_prefix}: {Convert.ToString(_scoreCount)}";
    }

}
