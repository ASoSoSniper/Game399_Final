using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimberHand : MonoBehaviour
{
    public OVRInput.Controller controller = OVRInput.Controller.None;

    [SerializeField] GameObject grabPoint = null;
    [SerializeField] Climber climber = null;
    Vector3 lastPosition = Vector3.zero;

    public Vector3 Delta { get; set; } = Vector3.zero;

    public List<GameObject> grabObjects = new List<GameObject>();

    private void Awake()
    {
        climber = GetComponentInParent<Climber>();
    }

    private void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            Grabpoint();
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            ReleasePoint();
        }
    }

    private void FixedUpdate()
    {
        lastPosition = transform.position;
    }
    private void LateUpdate()
    {
        Delta = lastPosition - transform.position;
    }

    void Grabpoint()
    {
        GameObject nearestPoint = null;
        float closest = Mathf.Infinity;
        for (int i = 0; i < grabObjects.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, grabObjects[i].transform.position);
            if (distance < closest)
            {
                nearestPoint = grabObjects[i];
                closest = distance;
            }
        }

        grabPoint = nearestPoint;
        climber.SetHand(this);
    }

    public void ReleasePoint()
    {
        grabPoint = null;
        climber.ClearHand();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ClimbPoint"))
        {
            AddPointToList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ClimbPoint"))
        {
            RemovePointFromList(other.gameObject);
        }
    }

    void AddPointToList(GameObject point)
    {
        if (!grabObjects.Contains(point))
        {
            grabObjects.Add(point);
        }
    }

    void RemovePointFromList(GameObject point)
    {
        if (grabObjects.Contains(point))
        {
            grabObjects.Remove(point);
        }
    }
}