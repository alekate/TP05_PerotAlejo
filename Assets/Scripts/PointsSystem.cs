using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    [SerializeField] private InitialPlayerData playerData;
    [SerializeField] private UIController UI;

    public float currentPoints;
    private void Awake()
    {
        currentPoints = playerData.totalPoints;
    }

    private void Update()
    {
        UI.UpdatePoints();
    }

}
