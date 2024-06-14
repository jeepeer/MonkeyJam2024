using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Player player;

    void Update()
    {
        if (!player) 
        {
            Debug.LogError($"Player not found");
            return; 
        }
        transform.position = player.transform.position + new Vector3(0, 1, -1) * 2.0f;
    }
}
