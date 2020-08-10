using System.Collections.Generic;
using UnityEngine;

public abstract class TrajectoryRenderer : MonoBehaviour
{

    #region Field Declarations

    [SerializeField] private protected GameObject dotPrefab;
    private protected List<GameObject> _instantiatedDots = new List<GameObject>() { };
    private protected List<Transform> _instantiatedDotsPosition = new List<Transform>() { };
    private protected Vector2 _previousThrowVector;
    private protected Vector2 _gravity;
    
    #endregion

    private void Awake()
    {
        _gravity = Physics2D.gravity;
    }

    #region Controllers

    public abstract void DrawTrajectory(Vector2 origin, Vector2 throwVector);

    public abstract void DeleteTrajectory();

    private protected virtual void CreateDots(int dotsCount)
    {
        for (int i = 0; i < dotsCount; i++)
        {
            GameObject newDot = Instantiate(dotPrefab, gameObject.transform);
            newDot.SetActive(false);
            _instantiatedDots.Add(newDot);
            _instantiatedDotsPosition.Add(newDot.transform);
        }
    }

    private protected virtual void DeleteDots(int dotsCount)
    {
        if (_instantiatedDots.Count > 0)
        {
            for (int i = 0; i < dotsCount; i++)
            {
                int pos = _instantiatedDots.Count - 1;
                Destroy(_instantiatedDots[pos]);
                _instantiatedDots.RemoveAt(pos);
                _instantiatedDotsPosition.RemoveAt(pos);
            }
        }
        
    }


    #endregion

    #region Calculations

    private protected virtual Vector3[] CalculateDotsPositions(Vector2 origin, Vector2 throwVector, int dotsCount, float timeStep) 
    {
        Vector3[] dotsPositions = new Vector3[dotsCount];

        for (int i = 0; i < dotsPositions.Length; i++)
        {
            float time = i * timeStep;

            dotsPositions[i] = (origin + throwVector * time) + _gravity * time * time / 2f;
        }

        return dotsPositions;
    }

    private protected virtual void RelocateDots(List<Transform> instantiatedDots, Vector3[] dotsPositions)
    {
        for (int i = 0; i < instantiatedDots.Count; i++)
        {
            instantiatedDots[i].position = dotsPositions[i];
        }
    }

    #endregion

}
