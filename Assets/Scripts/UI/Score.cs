using Jaba.Thrower.Helpers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Jaba.Thrower.UI
{
    public class Score : MonoBehaviour
    {
        #region Varibles

        [SerializeField]
        private string _prefix = "Score";

        private Text _scoreLabel;

        public int scoreCount { get; private set; } = -1;

        #endregion

        #region BuiltIn Methods

        #region Subscribe/Unsubscribe

        private void OnEnable()
        {
            SceneEventBroker.OnTargetHitted += UpdateScore;
            SceneEventBroker.OnCleanTargetHitted += UpdateScoreCleanHit;
            SceneEventBroker.OnGameOver += SaveScores;
        }

        private void OnDisable()
        {
            SceneEventBroker.OnTargetHitted -= UpdateScore;
            SceneEventBroker.OnCleanTargetHitted -= UpdateScoreCleanHit;
            SceneEventBroker.OnGameOver -= SaveScores;
        }

        #endregion

        private void Awake()
        {
            _scoreLabel = GetComponent<Text>();
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
            _scoreLabel.text = $"{_prefix}{Convert.ToString(scoreCount)}";
        }

        private void UpdateScoreCleanHit()
        {
            scoreCount += 2;
            _scoreLabel.text = $"{_prefix}{Convert.ToString(scoreCount)}";
        }

        private void SaveScores() => ProgressKeeper.UpdateMaxScore(scoreCount);

        #endregion

    }
}