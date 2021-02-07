using UnityEngine;

namespace Jaba.Thrower.Trajectory
{
    public class FixedTrajectoryRenderer : TrajectoryRenderer
    {
        #region Variables

        [SerializeField]
        private int _dotsCount = 10;

        #endregion

        #region BuiltIn Methods

        private void Start()
        {
            CreateDots(_dotsCount);
        }

        #endregion

        #region Overridden Methods

        public override void DrawTrajectory(Vector2 origin, Vector2 throwVector)
        {
            if (_previousThrowVector != throwVector)
            {
                RelocateDots(_instantiatedDotsPosition, CalculateDotsPositions(origin, throwVector, _dotsCount, 0.1f));

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

    }
}