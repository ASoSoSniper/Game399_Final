using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    public float sensitivity = 45f;

    ClimberHand currentHand;
    CharacterController controller;
    Rigidbody rigidBody;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        Vector3 movement = Vector3.zero;
        if (currentHand)
        {
            movement += currentHand.Delta * sensitivity;
            Debug.Log(movement);
        }
        else
        {
            movement = rigidBody.velocity;
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
}
