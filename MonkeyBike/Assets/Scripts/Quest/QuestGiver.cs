using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject cameraPosition;
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private Quest quest;
    private Player player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Mathf.Log(playerLayer.value, 2))
        {
            player = collision.gameObject.GetComponentInChildren<Player>();
            if (player) { StartQuest(player); }
        }
    }

    private void StartQuest(Player player)
    {
        StartQuestGiverCamera(player);
        
        player.StopMovement();
        player.SetPlayerPosition(playerPosition.transform.position);

        quest.StartQuest(player);
    }

    private void StartQuestGiverCamera(Player player)
    {
        Vector3 lookRotation = gameObject.transform.position;
        player.SetCameraPosition(lookRotation, cameraPosition.transform.position);
    }

    public void StartGameplay()
    {
        player.StartGameplay();
    }
}
