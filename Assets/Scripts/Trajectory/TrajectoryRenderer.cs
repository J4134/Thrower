using System.Collections.Generic;
using UnityEngine;

public abstract class TrajectoryRenderer : MonoBehaviour
{

    #region Field Declarations

    private protected List<GameObject> _instantiatedDots = new List<GameObject>() { };
    private protected List<Transform> _instantiatedDotsPosition = new List<Transform>() { };
    private protected Vector2 _previousThrowVector;
    private protected Vector2 _gravity;

    #endregion

    #region Controllers

    public abstract void DrawTrajectory(Vector2 origin, Vector2 throwVector);

    public abstract void DeleteTrajectory();

    #endregion

    #region Calculations

    private protected Vector3[] CalculateDotsPositions(Vector2 origin, Vector2 throwVector, int dotsCount, float timeStep) 
    {
        Vector3[] dotsPositions = new Vector3[dotsCount];

        for (int i = 0; i < dotsPositions.Length; i++)
        {
            float time = i * timeStep;

            dotsPositions[i] = (origin + throwVector * time) + _gravity * time * time / 2f;
        }

        return dotsPositions;
    }

    private protected void RelocateDots(List<Transform> instantiatedDots, Vector3[] dotsPositions)
    {
        for (int i = 0; i < instantiatedDots.Count; i++)
        {
            instantiatedDots[i].position = dotsPositions[i];
        }
    }

    #endregion
}
