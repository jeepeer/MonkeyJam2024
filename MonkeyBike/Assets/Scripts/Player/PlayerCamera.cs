using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //[SerializeField] private Player player;
    [SerializeField] private Test player;
    [SerializeField] private float mouseSensitivity;
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
        HandleTurning();
    }

    private void HandleTurning()
    {
        mouseXAxis += Input.GetAxis("Mouse X") * mouseSensitivity; 
        mouseYAxis -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        //mouseXAxis = Mathf.Clamp(mouseXAxis, -90, 90);
        mouseYAxis = Mathf.Clamp(mouseYAxis, -60, 30);

        Vector2 v = new Vector2(mouseXAxis, mouseYAxis);

        var x = Quaternion.AngleAxis(v.x, Vector3.up);
        var y = Quaternion.AngleAxis(v.y, Vector3.right);

        transform.localRotation = x * y;
    }

    public void LookAtQuestGiver(Vector3 lookDirection, Vector3 position)
    {
        turn = true;
        currentLookDirection = lookDirection;
        transform.localPosition = position;
    }

    private void TurnToQuestGiver()
    {
        transform.LookAt(currentLookDirection, Vector3.up);
    }

    public void PauseCamera()
    {
        pause = true;
    }

    public void StartCamera()
    {
        turn = false;
        transform.localPosition = Vector3.up;
    }
}