using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackPlayer : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Mathf.Log(playerLayer.value, 2))
        {
            other.GetComponent<Test>()?.KnockBack(transform.position);
        }
    }
}
