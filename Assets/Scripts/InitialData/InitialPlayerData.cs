using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InitialPlayerData", menuName = "Player/Data", order = 1)]
public class InitialPlayerData : ScriptableObject
{
    [Header("PlayerInput")]
    public KeyCode jumpKey = KeyCode.UpArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode downKey = KeyCode.DownArrow;

    public KeyCode shootKey = KeyCode.Z;

    [Header("JumpHeight")]
    public float jumpForce = 25f;

    [Header("MovementSpeed")]
    public float movementSpeed = 10f;

    [Header("Health")]
    public float playerHealth = 4f; //Cada enemigo quita 1HP;

    [Header("Points")]
    public float totalPoints;

}
