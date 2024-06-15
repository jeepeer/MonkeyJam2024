using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;

    [SerializeField] private float driftSpeed;
    [SerializeField] private float driftStaminaDrain;
    [SerializeField] private float minDriftStamina;
    [SerializeField] private float driftTimer;
    [SerializeField] private float driftTimeAmountlol;

    [SerializeField] private float turnSpeed;
    [SerializeField] private float breakAmount;

    [SerializeField] private float staminaDrain;

    [SerializeField] private float stamina = 100.0f;

    [SerializeField] private LayerMask knockBack;
    [SerializeField] private float knockBackForce;

    private float staminaRecovery;
    private bool recoverStamina = false;

    private int pedal = 0;

    private bool isGrounded;
    private Vector3 forward;
    private Gear myGear = Gear.Gear01;
    private BikeState myState = BikeState.Still;

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
        driftTimer = Mathf.Clamp(driftTimer + 0.5f * Time.deltaTime, 0.0f, 3.0f);
        if (myState == BikeState.Cruising || myState == BikeState.Still) 
        {
            stamina = Mathf.Clamp(stamina + 0.5f * Time.deltaTime, 0.0f, 100.0f);
            pedal = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Mathf.Log(knockBack.value, 2))
        {
            Vector3 e = transform.position - collision.transform.position;
            e.y = 0;
            rigidbody.AddForce(e.normalized * knockBackForce);
        }
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && CanPedal(0)) { AddSpeed(); }
        else if (Input.GetKeyDown(KeyCode.D) && CanPedal(1)) { AddSpeed(); }

        if (Input.GetKey(KeyCode.Space)) { Break(); }

        if (Input.GetKey(KeyCode.LeftShift)) { Drift(true); }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) { Drift(); }
    }

    private void HandleGearInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) { ChangeGear(1); }
        else if (Input.GetKeyDown(KeyCode.S)) { ChangeGear(-1); }
    }
    private void ChangeGear(int value)
    {
        if (stamina <= 0.0f) { return; }

        myGear = (Gear)Mathf.Clamp((float)myGear + value, 0.0f, (float)Gear.Gear03);
        turnSpeed = GetSavedTurnSpeed;
    }

    public void AddSpeed()
    {
        // TODO : fix -= staminaDrain when 0 stamina
        myState = BikeState.Pedaling;

        if (stamina <= staminaDrain)
        {
            myGear = Gear.Gear01;
        }

        if (!playerCamera)
        {
            Debug.LogError($"PlayerCamera not found");
            return;
        }

        rigidbody.AddForce(forward.normalized * SpeedToAdd, ForceMode.Force);
        stamina -= staminaDrain;
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

    private void Break()
    {
        rigidbody.velocity -= new Vector3(breakAmount, breakAmount, breakAmount);
    }

    private void Drift(bool keyDown = false)
    {
        if (stamina <= minDriftStamina || driftTimer <= 0.0f) { return; }

        if (!keyDown)
        {
            turnSpeed = GetSavedTurnSpeed;
            return;
        }

        if (driftTimer > 0.0f)
        {
            turnSpeed = driftSpeed; // fix this it's called every frame
            stamina -= driftStaminaDrain * Time.deltaTime;
            driftTimer -= 1 * Time.deltaTime;
        }
    }

    private void HandlePlayerForward()
    {
        if (!isGrounded) { return; }

        forward = Vector3.MoveTowards(forward.normalized, playerCamera.transform.forward.normalized, turnSpeed * Time.deltaTime);
        forward.y = 0;
        rigidbody.velocity = forward.normalized * rigidbody.velocity.magnitude;
    }

    public void AddStamina(float amount)
    {
        stamina += amount;
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