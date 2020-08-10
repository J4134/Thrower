using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTrajectoryRenderer : TrajectoryRenderer
{
    [SerializeField] private int _dotsCount = 10;
    [SerializeField] private GameObject dotPrefab;

    private void Awake()
    {
         _gravity = Physics2D.gravity;
    }

    private void Start()
    {
        CreateDots(_dotsCount);
    }

    private void CreateDots(int dotsCount)
    {
        for (int i = 0; i < dotsCount; i++)
        {
            GameObject newDot = Instantiate(dotPrefab, gameObject.transform);
            newDot.SetActive(false);
            _instantiatedDots.Add(newDot);
            _instantiatedDotsPosition.Add(newDot.transform);
        }
    }

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


}
