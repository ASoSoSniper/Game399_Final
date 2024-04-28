using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    public float sensitivity = 45f;

    ClimberHand currentHand;
    CharacterController controller;
    Rigidbody rigidBody;
    CapsuleCollider capsuleCollider;

    Vector3 lastPosition = Vector3.zero;
    Vector3 spaceVelocity = Vector3.zero;

    Vector3 initialPos = Vector3.zero;

    [SerializeField] bool inGrabMode = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        lastPosition = transform.position;

        initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        spaceVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;

        lastPosition = transform.position;
    }

    private void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.A)) ResetMovement();
    }

    void CalculateMovement()
    {
        ToggleGrabMode(currentHand);

        if (!inGrabMode) return;

        Vector3 movement = Vector3.zero;

        if (currentHand)
        {
            movement += currentHand.Delta * sensitivity;
        }
        controller.Move(movement * Time.deltaTime);
    }

    public void SetHand(ClimberHand hand)
    {
        if (currentHand)
            currentHand.ReleasePoint();

        currentHand = hand;
    }

    public void ClearHand()
    {
        currentHand = null;
    }

    void ToggleGrabMode(bool active)
    {
        if (active == inGrabMode) return;

        controller.enabled = active;

        capsuleCollider.enabled = !active;

        inGrabMode = active;

        if (active)
        {
            rigidBody.velocity = Vector3.zero;
        }
        else
        {
            rigidBody.AddForce(spaceVelocity, ForceMode.Impulse);
            //rigidBody.velocity = spaceVelocity;
        }
    }

    void ResetMovement()
    {
        ClearHand();
        transform.position = initialPos;
        rigidBody.velocity = Vector3.zero;
    }
}
