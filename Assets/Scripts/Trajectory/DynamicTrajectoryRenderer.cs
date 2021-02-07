using Jaba.Thrower.Helpers;
using UnityEngine;

namespace Jaba.Thrower.Trajectory
{
    public class DynamicTrajectoryRenderer : TrajectoryRenderer
    {
        #region Variables

        [SerializeField]
        private float _step = 0.1f;

        #endregion

        #region BuiltIn Methods

        #region Subscribe/Unsubscribe

        private void OnEnable()
        {
            SceneEventBroker.OnGameOver += DeleteTrajectory;
            SceneEventBroker.OnUnpaused += DeleteTrajectory;
        }

        private void OnDisable()
        {
            SceneEventBroker.OnGameOver -= DeleteTrajectory;
            SceneEventBroker.OnUnpaused -= DeleteTrajectory;
        }

        #endregion

        #endregion

        #region Custom Methods

        private int DotsCount(Vector2 throwVector) => Mathf.RoundToInt(throwVector.magnitude);

        #region Overridden Methods

        public override void DrawTrajectory(Vector2 origin, Vector2 throwVector)
        {
            if (_previousThrowVector != throwVector)
            {
                int newDotsCount = DotsCount(throwVector);
                int currentDotsCount = _instantiatedDots.Count;

                if (currentDotsCount < newDotsCount)
                {
                    CreateDots(newDotsCount - currentDotsCount);
                }
                else if (currentDotsCount > newDotsCount)
                {
                    DeleteDots(currentDotsCount - newDotsCount);
                }

                RelocateDots(_instantiatedDotsPosition, CalculateDotsPositions(origin, throwVector, _instantiatedDots.Count, _step));

                foreach (GameObject dot in _instantiatedDots)
                {
                    dot.SetActive(true);
                }
            }
        }

        public override void DeleteTrajectory()
        {
            if (_instantiatedDots.Count > 0)
            {
                foreach (GameObject dot in _instantiatedDots)
                {
                    dot.SetActive(false);
                }
            }
        }

        #endregion

        #endregion

    }
}