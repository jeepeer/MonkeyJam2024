using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float driftSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float breakAmount;
    [SerializeField] private float staminaDecreaseAmount;

    private int pedal = 0;
    private float stamina = 100.0f;

    private bool isGrounded;
    private Vector3 forward;
    private Gear myGear = Gear.Gear01;
    private Rigidbody rigidbody;

    private float SpeedToAdd
    {
        get
        {
            switch (myGear)
            {
                case Gear.Gear01:
                    return 30.0f;
                case Gear.Gear02:
                    return 60.0f;
                case Gear.Gear03:
                    return 90.0f;
                default:
                    Debug.LogError($"Unexpected Gear : {myGear}");
                    return 30.0f;
            }
        }
    }

    private float GetSavedTurnSpeed
    {
        get
        {
            switch (myGear)
            {
                case Gear.Gear01:
                    return 1.0f;
                case Gear.Gear02:
                    return 0.6f;
                case Gear.Gear03:
                    return 0.4f;
                default:
                    Debug.LogError($"Unexpected Gear : {myGear}");
                    return 1.0f;
            }
        }
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(CheckGrounded());
    }

    private void Update()
    {
        HandleMovementInput();
        HandleGearInput();
        HandlePlayerForward();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && CanPedal(0)) { AddSpeed(); }
        else if (Input.GetKeyDown(KeyCode.D) && CanPedal(1)) { AddSpeed(); }

        if (Input.GetKey(KeyCode.Space)) { Break(); }

        if (Input.GetKeyDown(KeyCode.LeftShift)) { Drift(true); }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) { Drift(); }
    }

    private void HandleGearInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) { ChangeGear(1); }
        else if (Input.GetKeyDown(KeyCode.S)) { ChangeGear(-1); }
    }

    public void AddSpeed()
    {
        if (stamina <= 0.0f)
        {
            myGear = Gear.Gear01;
        }
        if (!playerCamera)
        {
            Debug.LogError($"PlayerCamera not found");
            return;
        }

        rigidbody.AddForce(forward.normalized * SpeedToAdd, ForceMode.Acceleration);
        stamina -= staminaDecreaseAmount;
        Debug.Log($"stamina : {stamina}");
    }

    private void ChangeGear(int value)
    {
        if (stamina <= 0.0f) { return; }

        myGear = (Gear)Mathf.Clamp((float)myGear + value, 0.0f, (float)Gear.Gear03);
        turnSpeed = GetSavedTurnSpeed;
    }

    private bool CanPedal(int value)
    {
        if ((pedal & (1 << value)) != 0) { return false; }
        if (pedal == 0) 
        { 
            pedal = (1 << value);
            return true;
        }
        
        pedal ^= 0b_11;
        return true;
    }

    private void HandlePlayerForward()
    {
        if (!isGrounded) { return; }

        forward = Vector3.MoveTowards(forward.normalized, playerCamera.transform.forward.normalized, turnSpeed * Time.deltaTime);
        forward.y = 0;
        rigidbody.velocity = forward.normalized * rigidbody.velocity.magnitude;
    }

    private void Break()
    {
        rigidbody.velocity -= new Vector3(breakAmount, breakAmount, breakAmount);
    }

    private void Drift(bool keyDown = false)
    {
        if (stamina <= 10.0f) { return; }
        if (!keyDown)
        {
            turnSpeed = GetSavedTurnSpeed;
            return;
        }

        turnSpeed = driftSpeed;
        stamina -= 10.0f;
    }

    private IEnumerator CheckGrounded()
    {
        while (true)
        {
            if (Physics.Raycast(transform.position, Vector3.down, 0.5f))
            {
                isGrounded = true;
            }
            else { isGrounded = false; }

            yield return new WaitForSeconds(1.0f);
        }
    }
}