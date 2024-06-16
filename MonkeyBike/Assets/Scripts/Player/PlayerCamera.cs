using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float turnSpeed;
    private float mouseXAxis = 0.0f;
    private float mouseYAxis = 0.0f;
    private bool pause = false;
    private bool turn = false;
    private Vector3 currentLookDirection;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (pause) { return; }
        if (turn) 
        { 
            TurnToQuestGiver();
            return;
        }
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
        Vector3 vb = new Vector3(player.transform.position.x, player.transform.position.y +1, player.transform.position.z);
        transform.position = vb;
    }

    public void LookAtQuestGiver(Vector3 lookDirection, Vector3 position)
    {
        turn = true;
        currentLookDirection = lookDirection;
        transform.position = position;
    }

    private void TurnToQuestGiver()
    {
        transform.LookAt(currentLookDirection, Vector3.up);
        transform.position = Vector3.zero;
    }

    public void PauseCamera()
    {
        pause = true;
    }

    public void StartCamera()
    {
        turn = false;
    }
}