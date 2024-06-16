using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject questUI;

    private Test playerMovement;

    public List<QuestItems> questItems;
    
    private void Start()
    {
        playerMovement = GetComponent<Test>();
    }

    public void SetPlayerPosition(Vector3 position)
    {
        transform.parent.position = position;
    }

    public void StopMovement()
    {
        // please stop
        playerMovement.rigidbody.velocity = Vector3.zero;
        playerMovement.force = 0.0f;
        playerMovement.pause = true;
        playerMovement.rigidbody.isKinematic = true;

        questUI.SetActive(true);
        gameUI.SetActive(false);
    }

    public void StartGameplay()
    {
        playerMovement.pause = false;
        playerMovement.rigidbody.isKinematic = false;
        playerCamera.StartCamera();

        questUI.SetActive(false);
        gameUI.SetActive(true);
    }

    public void SetCameraPosition(Vector3 lookRotation, Vector3 position)
    {
        playerCamera.LookAtQuestGiver(lookRotation, position);
    }

    public void AddItem(QuestItems item)
    {
        questItems.Add(item);
    }

    public void RewardMoreSpeed()
    {
        playerMovement.rigidbody.maxLinearVelocity = 50.0f;
        playerMovement.speed = 3.0f;
    }
}
