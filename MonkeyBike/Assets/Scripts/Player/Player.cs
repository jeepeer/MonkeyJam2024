using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerCamera playerCamera;
    private PlayerMovement playerMovement;

    public List<QuestItems> questItems;
    
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void AddStamina(float amount)
    {
        playerMovement.AddStamina(amount);
    }

    public void SetPlayerPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void StopMovement()
    {
        playerMovement.PauseMovement();
    }

    public void StartGameplay()
    {
        playerMovement.StartMovement();
        playerCamera.StartCamera();
    }

    public void SetCameraPosition(Vector3 lookRotation, Vector3 position)
    {
        playerCamera.LookAtQuestGiver(lookRotation, position);
    }

    public void AddItem(QuestItems item)
    {
        questItems.Add(item);
    }
}
