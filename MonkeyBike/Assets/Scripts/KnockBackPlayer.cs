using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackPlayer : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Mathf.Log(playerLayer.value, 2))
        {
            collision.gameObject.GetComponentInChildren<Test>()?.KnockBack(transform.position);
        }
    }
}
