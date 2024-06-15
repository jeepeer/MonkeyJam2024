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

    public void AddStamina(float amount)
    {
        playerMovement.AddStamina(amount);
    }

    public void KnockBack()
    {

    }

    public void Stop()
    {

    }

    public void SetCameraPosition(Vector3 position)
    {
        playerMovement.playerCamera.transform.position = position;
    }
}
