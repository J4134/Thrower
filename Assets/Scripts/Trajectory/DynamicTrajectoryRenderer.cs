using UnityEngine;

public class DynamicTrajectoryRenderer : TrajectoryRenderer
{
    #region Field Declarations

    [SerializeField] 
    private float _step = 0.1f;

    private int DotsCount(Vector2 throwVector) => Mathf.RoundToInt(throwVector.magnitude);

    #endregion

    #region BuiltIn Methods

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

}
