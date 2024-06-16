using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody rigidbody;
    Vector3 movePos;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movePos += transform.forward * 10.0f * Time.deltaTime;
            rigidbody.MovePosition(movePos);
        }
    }
}
