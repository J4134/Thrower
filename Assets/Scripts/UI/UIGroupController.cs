using Jaba.Thrower.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Jaba.Thrower.UI
{
    public class UIGroupController : MonoBehaviour
    {
        #region Variables

        [SerializeField]
        private GameObject _inGameGroup;

        [SerializeField]
        private GameObject _onGameOverGroup;

        [SerializeField]
        private GameObject _pauseGroup;

        [SerializeField]
        private Text _gameOverLabel;

        [SerializeField]
        private Score _score;

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
            _onGameOverGroup.SetActive(true);
            _inGameGroup.SetActive(false);
            _pauseGroup.SetActive(false);
        }

        private void OnStartUI()
        {
            _inGameGroup.SetActive(true);
            _onGameOverGroup.SetActive(false);
            _pauseGroup.SetActive(false);
        }

        private void OnPausedUI()
        {
            _pauseGroup.SetActive(true);
            _inGameGroup.SetActive(false);
            _onGameOverGroup.SetActive(false);
        }

        private void RewriteGameOverText()
        {
            _gameOverLabel.text = "You lose :(" + "\n\n" + "Score: " + _score.scoreCount + "\n" + "Best score: " + PlayerPrefs.GetInt("maxScore");
        }

        #endregion
    }
}