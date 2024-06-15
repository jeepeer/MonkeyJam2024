using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject cameraPosition;
    [SerializeField] private Quest quest;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Mathf.Log(playerLayer.value, 2))
        {
            if (collision.gameObject.TryGetComponent<Player>(out Player player))
            {
                StartQuestGiverCamera(player);
                player.Stop();
                player.KnockBack();
            }

            DisplayQuestText();
        }
    }

    private void StartQuestGiverCamera(Player player)
    {
        player.SetCameraPosition(cameraPosition.transform.position);
    }

    private void DisplayQuestText()
    {

    }
}
