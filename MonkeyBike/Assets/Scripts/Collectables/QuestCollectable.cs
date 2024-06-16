using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollectable : MonoBehaviour
{
    [SerializeField] private QuestItems questItem;
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Mathf.Log(playerLayer.value, 2))
        {
            other.GetComponent<Player>()?.AddItem(questItem);
            Destroy(gameObject);
        }
    }
}