using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float randSpeedMin = 0.1f;
    [SerializeField] float randSpeedMax = 0.5f;

    [SerializeField] float rotationSpeed = 0f;

    Vector3 rotDirection = Vector3.zero;

    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rotDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        rotationSpeed = Random.Range(randSpeedMin, randSpeedMax);
    }

    private void FixedUpdate()
    {
        rigidBody.angularVelocity = Vector3.ClampMagnitude(rotDirection * rotationSpeed, rotationSpeed);
    }
}
