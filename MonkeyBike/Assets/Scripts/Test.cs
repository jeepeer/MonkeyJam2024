using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] public float force;
    [SerializeField] private float rotationSpeed;
    private float maxRotationSpeed = 20000.0f;
    private float defaultRotationSpeed;

    public float speed = 2.0f;
    private float cruiseTimer;
    private float cruiseCooldown = 1.0f;
    private float maxForce = 30.0f;
    private int pedal;
    
    private float drift = 6000.0f;
    private float driftTimer;
    private float maxDriftTime = 3.0f;

    public bool pause = false;

    private BikeState bikeState;
    public Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        defaultRotationSpeed = rotationSpeed;
        rigidbody.maxLinearVelocity = 12.0f;
    }

    void Update()
    {
        if (pause) { return; }

        if (bikeState == BikeState.Default)
        {
            driftTimer += 1.5f * Time.deltaTime;
            driftTimer = Mathf.Clamp(driftTimer, 0.0f, maxDriftTime);
        }
        HandleMovement();
        HandleDrag();
        RotateToCameraForward();

        rigidbody.velocity += Physics.gravity * 2 * Time.deltaTime;
        rigidbody.velocity += transform.forward * force * Time.deltaTime;
        if (rigidbody.velocity.magnitude <= 1.0f) 
        {
            // fix later lmao hihi
            Vector3 newForward = playerCamera.transform.forward;
            newForward.y = 0.0f;
            transform.forward = newForward; 
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) && CanPedal(0)) { AddForce(); }
        else if (Input.GetKeyDown(KeyCode.D) && CanPedal(1)) { AddForce(); }

        if (Input.GetKey(KeyCode.LeftShift)) { Drift(); }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) 
        { 
            bikeState = BikeState.Default;
            rotationSpeed = defaultRotationSpeed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Break();
        }
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

    private void AddForce()
    {
        force += speed;
        force = Mathf.Clamp(force, 0.0f, maxForce);
        cruiseTimer = cruiseCooldown;
    }
    
    private void Drift()
    {
        if (driftTimer <= 0.0f) { return; }

        bikeState = BikeState.Drifting;

        driftTimer -= 1.0f * Time.deltaTime;

        rotationSpeed += drift * Time.deltaTime;
        rotationSpeed = Mathf.Clamp(rotationSpeed, 0.0f, maxRotationSpeed);;
    }

    private void Break()
    {
        force -= 100.0f * Time.deltaTime;
        force = Mathf.Clamp(force, 0.0f, maxForce);
    }

    private void RotateToCameraForward()
    {
        Quaternion targetRotation = playerCamera.transform.localRotation;
        targetRotation.x = 0.0f;
        targetRotation.z = 0.0f;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleDrag()
    {
        cruiseTimer -= 1.0f * Time.deltaTime;
        if (cruiseTimer <= 0.0f)
        {
            force -= 1.0f * Time.deltaTime;
            force = Mathf.Clamp(force, 0.0f, maxForce);
        }
    }

    public void KnockBack(Vector3 hitDirection)
    {
        rigidbody.AddForce(hitDirection - transform.parent.position * 20.0f, ForceMode.Impulse);
        force = 0.0f;
    }
}