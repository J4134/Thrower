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
        EventBroker.HitTarget += UpdateScore;
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
        EventBroker.HitTarget -= UpdateScore;
    }

    private void UpdateScore()
    {
        _scoreCount += 1;
        scoreLabel.text = $"{_prefix}: {Convert.ToString(_scoreCount)}";
    }


}
