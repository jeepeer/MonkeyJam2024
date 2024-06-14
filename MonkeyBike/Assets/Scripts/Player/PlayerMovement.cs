using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Gear myGear = Gear.Gear01;
    private int pedal = 0;
    private float stamina = 100.0f;

    private float SpeedToAdd
    {
        get
        {
            switch (myGear)
            {
                case Gear.Gear01:
                    return 10.0f;
                case Gear.Gear02:
                    return 30.0f;
                case Gear.Gear03:
                    return 50.0f;
                default:
                    Debug.LogError($"Unexpected Gear : {myGear}");
                    return 10.0f;
            }
        }
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleTurning();
        HandleGearInput();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && CanPedal(0))
        {
            AddSpeed();
        }
        else if (Input.GetKeyDown(KeyCode.D) && CanPedal(1))
        {
            AddSpeed();
        }
    }

    private void HandleTurning()
    {

    }

    private void HandleGearInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ChangeGear(1);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeGear(-1);
        }
    }

    public void AddSpeed()
    {
        rigidbody.AddForce(Vector3.forward * SpeedToAdd, ForceMode.Force);
        Debug.Log($"SpeedToAdd : {SpeedToAdd}");
    }

    private void ChangeGear(int value)
    {
        myGear = (Gear)Mathf.Clamp((float)myGear + value, 0.0f, (float)Gear.Gear03);
        Debug.Log($"Gear : {myGear}");
    }

    private bool CanPedal(int value)
    {
        if ((pedal & (1 << value)) != 0 || stamina <= 0.0f) { return false; }
        if (pedal == 0) 
        { 
            pedal = (1 << value);
            return true;
        }
        
        pedal ^= 0b_11;
        return true;
    }
}