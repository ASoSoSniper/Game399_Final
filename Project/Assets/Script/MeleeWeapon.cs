using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] Collider hitBox;
    public bool grabbed = false;
    [SerializeField] float minimumRotMagnitude = 20f;

    Vector3 lastPosition = Vector3.zero;
    Quaternion lastRotation;

    Vector3 spaceVelocity = Vector3.zero;
    Vector3 spaceRotation;

    Vector3 initPos;
    Quaternion initRot;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        initPos = transform.position;
        initRot = transform.rotation;

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = initPos;
            transform.rotation = initRot;
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        spaceVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;

        spaceRotation = (transform.rotation.eulerAngles - lastRotation.eulerAngles) / Time.fixedDeltaTime;
        //spaceRotation = Quaternion.Euler(0, 90, 0) * spaceRotation;

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    public void ToggleGrabMode(bool active)
    {
        if (grabbed == active) return;

        grabbed = active;

        if (active)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.isKinematic = true;
        }
        else
        {
            rigidBody.isKinematic = false;

            rigidBody.AddForce(spaceVelocity, ForceMode.Impulse);
            if (spaceRotation.magnitude >= minimumRotMagnitude) rigidBody.AddTorque(spaceRotation, ForceMode.Impulse);
        }
    }
}