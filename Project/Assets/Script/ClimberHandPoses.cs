using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimberHandPoses : MonoBehaviour
{
    public OVRInput.Controller controller = OVRInput.Controller.None;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            animator.SetBool("Grab", true);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            animator.SetBool("Grab", false);
        }
    }
}
