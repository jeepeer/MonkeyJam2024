using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private float stamina;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
}
