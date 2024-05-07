using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOnLook : MonoBehaviour
{
    [SerializeField] float dotTarget = 0.5f;
    [SerializeField] bool invert;
    [SerializeField] GameObject head;
    [SerializeField] GameObject objectToHide;

    [SerializeField] float showDelay = 0.5f;
    [SerializeField] float hideDelay = 0.5f;
    float currentShowTime = 0f;
    float currentHideTime = 0f;
    [SerializeField] bool debug;

    // Start is called before the first frame update
    void Start()
    {
        currentShowTime = showDelay;
        currentHideTime = hideDelay;
    }

    private void FixedUpdate()
    {
        float dot = Vector3.Dot(head.transform.forward, transform.right * (invert ? -1 : 1));
        if (debug) Debug.Log(dot);

        if (dot >= dotTarget)
        {
            currentHideTime = hideDelay;

            if (!objectToHide.activeSelf)
            {
                currentShowTime -= Time.deltaTime;
                if (currentShowTime <= 0)
                {
                    objectToHide.SetActive(true);
                }
            }
        }
        else
        {
            currentShowTime = showDelay;

            if (objectToHide.activeSelf)
            {
                currentHideTime -= Time.deltaTime;
                if (currentHideTime <= 0)
                {
                    objectToHide.SetActive(false);
                }
            }
        }
    }
}
