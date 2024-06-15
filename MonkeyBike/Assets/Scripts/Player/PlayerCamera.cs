using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float turnSpeed;
    private float mouseXAxis = 0.0f;
    private float mouseYAxis = 0.0f;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!player) 
        {
            Debug.LogError($"Player not found");
            return; 
        }
        HandleTurning();
    }

    private void HandleTurning()
    {
        mouseXAxis += Input.GetAxis("Mouse X") * turnSpeed; 
        mouseYAxis -= Input.GetAxis("Mouse Y") * turnSpeed;

        //mouseXAxis = Mathf.Clamp(mouseXAxis, -90, 90);
        mouseYAxis = Mathf.Clamp(mouseYAxis, -60, 30);

        Vector2 v = new Vector2(mouseXAxis, mouseYAxis);

        var x = Quaternion.AngleAxis(v.x, Vector3.up);
        var y = Quaternion.AngleAxis(v.y, Vector3.right);

        transform.localRotation = x * y;
        transform.position = player.transform.position;
    }
}