using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimberHand : MonoBehaviour
{
    public OVRInput.Controller controller = OVRInput.Controller.None;

    [SerializeField] GameObject grabPoint = null;
    [SerializeField] GameObject grip;
    [SerializeField] GameObject gripPrefab;
    [SerializeField] Climber climber = null;
    Vector3 lastPosition = Vector3.zero;

    public Vector3 Delta { get; set; } = Vector3.zero;

    public List<GameObject> grabObjects = new List<GameObject>();

    AudioSource audioSource;

    private void Awake()
    {
        climber = GetComponentInParent<Climber>();
    }

    private void Start()
    {
        lastPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
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
        if (grip) transform.position = grip.transform.position;

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

        if (grabPoint)
        {
            if (grabPoint.CompareTag("ClimbPoint"))
            {
                climber.SetHand(this);

                grip = Instantiate(gripPrefab, grabPoint.transform);
                grip.transform.position = transform.position;
            }
            else if (grabPoint.CompareTag("Grabbable"))
            {
                GrabWeapon();
            }
        }
    }

    public void ReleasePoint()
    {
        if (grabPoint)
        {
            if (grabPoint.CompareTag("ClimbPoint"))
            {
                climber.ClearHand();
            }
            else if (grabPoint.CompareTag("Grabbable"))
            {
                ReleaseWeapon();
            }
        }

        Destroy(grip);
        grip = null;
        grabPoint = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ClimbPoint") || other.gameObject.CompareTag("Grabbable"))
        {
            AddPointToList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ClimbPoint") || other.gameObject.CompareTag("Grabbable"))
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

    void GrabWeapon()
    {
        grabPoint.transform.parent = transform;
        grabPoint.transform.localPosition = Vector3.zero;
        grabPoint.transform.rotation = transform.rotation;

        MeleeWeapon weapon = grabPoint.GetComponent<MeleeWeapon>();
        if (weapon) weapon.ToggleGrabMode(true);

        OxygenTank oxygenTank = grabPoint.GetComponent<OxygenTank>();
        if (oxygenTank) oxygenTank.GrabOxygen();
    }

    void ReleaseWeapon()
    {
        if (!grabPoint) return;

        grabPoint.transform.parent = null;

        MeleeWeapon weapon = grabPoint.GetComponent<MeleeWeapon>();
        if (weapon) weapon.ToggleGrabMode(false);
    }

    public void PlayGrabSound(List<AudioClip> grabSounds)
    {
        if (!audioSource) return;

        AudioClip randomClip = grabSounds[Random.Range(0, grabSounds.Count - 1)];
        
        audioSource.PlayOneShot(randomClip);
    }
}
