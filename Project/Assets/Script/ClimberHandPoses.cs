using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimberHandPoses : MonoBehaviour
{
    public OVRInput.Controller controller = OVRInput.Controller.None;

    public Animator animator;

    [SerializeField] bool test;
    [SerializeField] float testAlpha = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = 0f;

        if (!test)
        {
            input = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
        }
        else
        {
            input = Mathf.Clamp01(testAlpha);
        }

        animator.SetFloat("Blend", input);

        /*if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            animator.SetBool("Grab", true);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            animator.SetBool("Grab", false);
        }*/
    }
}
