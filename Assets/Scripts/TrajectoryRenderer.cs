using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{

    #region Field Declarations

    [SerializeField] private GameObject dotPrefab;

    [SerializeField] private DotsAmount _dotsAmount = DotsAmount.Fixed;
    [SerializeField] private int _dotsCount = 10;



    private enum DotsAmount { Fixed, Dynamic };
    
    private List<GameObject> _trajectoryDots = new List<GameObject>() { };
    private List<Transform> _trajectoryDotsPositins = new List<Transform>() { };

    private Vector3[] _dotsPositions;
    private Vector2 _previousThrowVector;

    private Vector2 _gravity;

    private void Start()
    {
        _gravity = Physics2D.gravity;
        CreateDots(_dotsAmount);
    }

    #endregion

    #region Controllers

    public void DrawTrajectory(Vector2 origin, Vector2 throwVector)
    {
        if (_previousThrowVector != throwVector)
        {
            if (_dotsAmount == DotsAmount.Fixed)
            {
                RelocateDots(_trajectoryDotsPositins, CalculateDotsPositions(origin, throwVector, _dotsCount, 0.1f));

                foreach (GameObject dot in _trajectoryDots)
                {
                    dot.SetActive(true);
                }
            }
            else if (_dotsAmount == DotsAmount.Dynamic)
            {

            }
        }
    }

    public void DeleteTrajectory()
    {
        if (_trajectoryDots.Count > 0)
        {
            if (_dotsAmount == DotsAmount.Dynamic)
            {
                // TO DO: переписать согласно новой логике.
                foreach (GameObject dot in _trajectoryDots)
                {
                    Destroy(dot);
                }

            }
            else if (_dotsAmount == DotsAmount.Fixed)
            {
                foreach (GameObject dot in _trajectoryDots)
                {
                    dot.SetActive(false);
                }
            }
        }

        // Нужно как-то очистить previousThrowVector, чтобы после броска траектория строилась заново, даже при совпадающих векторах.
        _previousThrowVector = new Vector2(0f, 0f);
    }

    private void CreateDots(DotsAmount dotsAmount)
    {
        if (dotsAmount == DotsAmount.Fixed)
        {
            for (int i = 0; i < _dotsCount; i++)
            {
                GameObject newDot = Instantiate(dotPrefab, gameObject.transform);
                newDot.SetActive(false);
                _trajectoryDots.Add(newDot);
                _trajectoryDotsPositins.Add(newDot.transform);
            }
        }
    }

    #endregion

    #region Calculations

    private Vector3[] CalculateDotsPositions(Vector2 origin, Vector2 throwVector, int dotsCount, float timeStep) 
    {
        Vector3[] dotsPositions = new Vector3[dotsCount];

        for (int i = 0; i < dotsPositions.Length; i++)
        {
            float time = i * timeStep;

            dotsPositions[i] = (origin + throwVector * time) + _gravity * time * time / 2f;
        }

        return dotsPositions;
    }

    private void RelocateDots(List<Transform> instantiatedDots, Vector3[] dotsPositions)
    {
        for (int i = 0; i < instantiatedDots.Count; i++)
        {
            instantiatedDots[i].position = dotsPositions[i];
        }
    }

    #endregion
}
