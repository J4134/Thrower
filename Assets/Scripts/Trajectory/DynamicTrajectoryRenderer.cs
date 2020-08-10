using UnityEngine;

public class DynamicTrajectoryRenderer : TrajectoryRenderer
{
    [SerializeField] private float _step = 0.1f;
    private int DotsCount(Vector2 throwVector) => Mathf.RoundToInt(throwVector.magnitude);


    #region Overridden Methods

    public override void DrawTrajectory(Vector2 origin, Vector2 throwVector)
    {
        if (_previousThrowVector != throwVector)
        {
            
            if (_instantiatedDots.Count < DotsCount(throwVector))
            {
                CreateDots(DotsCount(throwVector) - _instantiatedDots.Count);
            }
            else if (_instantiatedDots.Count > DotsCount(throwVector))
            {
                DeleteDots(_instantiatedDots.Count - DotsCount(throwVector));
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

    
}
