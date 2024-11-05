using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] PointsSystem pointsSystem;
    [SerializeField] private SoundController soundController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            pointsSystem.currentPoints++;
            soundController.PickupSFX();
        }
    }
}
