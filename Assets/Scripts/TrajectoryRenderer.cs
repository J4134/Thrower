using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{

    #region Field Declarations

    public GameObject dotPrefab;

    [SerializeField] private DotsAmount dotsAmount = DotsAmount.Fixed;
    [SerializeField] private int dotsCount = 10;

    private enum DotsAmount { Fixed, Dynamic };
    
    private List<GameObject> trajectoryDots = new List<GameObject>() { };
    private Vector3[] dotsPositions;
    private Vector2 previousThrowVector;

    private void Start()
    {
        if (dotsAmount == DotsAmount.Fixed)
        {
            for (int i = 0; i < dotsCount; i++)
            {
                GameObject newDot = Instantiate(dotPrefab, gameObject.transform);
                newDot.SetActive(false);
                trajectoryDots.Add(newDot);
            }
        }
    }

    #endregion

    #region Controllers

    public void DrawTrajectory(Vector2 origin, Vector2 throwVector)
    {
        if (previousThrowVector != throwVector)
        {
            RelocateDots(trajectoryDots, CalculateDotsPositions(origin, throwVector, dotsCount, 0.1f));

            foreach (GameObject dot in trajectoryDots)
            {
                dot.SetActive(true);
            }
        }
        else
        {
            //TODO: написать для dynamic

        }
    }

    public void DeleteTrajectory()
    {
        if (trajectoryDots.Count > 0)
        {
            if (dotsAmount == DotsAmount.Dynamic)
            {
                // TO DO: переписать согласно новой логике.
                foreach (GameObject dot in trajectoryDots)
                {
                    Destroy(dot);
                }
            }
            else if (dotsAmount == DotsAmount.Fixed)
            {
                foreach (GameObject dot in trajectoryDots)
                {
                    dot.SetActive(false);
                }
            }
        }

        // Нужно как-то очистить previousThrowVector, чтобы после броска траектория строилась заново, даже при совпадающих векторах.
        previousThrowVector = new Vector2(0f, 0f);
    }

    #endregion

    #region Calculations

    private Vector3[] CalculateDotsPositions(Vector2 origin, Vector2 throwVector, int dotsCount, float timeStep) 
    {
        Vector3[] dotsPositions = new Vector3[dotsCount];

        for (int i = 0; i < dotsPositions.Length; i++)
        {
            float time = i * timeStep;

            dotsPositions[i] = (origin + throwVector * time) + Physics2D.gravity * time * time / 2f;
        }

        return dotsPositions;
    }

    private void RelocateDots(List<GameObject> instantiatedDots, Vector3[] dotsPositions)
    {
        for (int i = 0; i < instantiatedDots.Count; i++)
        {
            instantiatedDots[i].transform.position = dotsPositions[i];
        }
    }

    #endregion
}
