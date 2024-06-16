using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyObject : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float staminaGain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Mathf.Log(playerLayer.value,2))
        {
            //other.GetComponent<Player>()?.AddStamina(staminaGain);
        }
    }
}
