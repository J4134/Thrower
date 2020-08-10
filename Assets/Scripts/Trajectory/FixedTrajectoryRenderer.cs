using UnityEngine;

public class FixedTrajectoryRenderer : TrajectoryRenderer
{
    #region Field Declarations

    [SerializeField] private int _dotsCount = 10;

    #endregion

    private void Start()
    {
        CreateDots(_dotsCount);
    }

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
