using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(HittableObject))]
public class HittableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HittableObject script = (HittableObject)target;
        if (GUILayout.Button("Repair"))
        {
            script.ToggleRepair(true);
        }
        if (GUILayout.Button("Break"))
        {
            script.ToggleRepair(false);
        }
    }
}

public class HittableObject : MonoBehaviour
{
    [SerializeField] Material normalMat;
    [SerializeField] Material hitMat;
    [SerializeField] MeshRenderer mesh;

    [SerializeField] GameObject waypoint;
    [SerializeField] ParticleSystem particle;

    [SerializeField] GameObject brokenSoundObject;
    [SerializeField] GameObject repairedSoundObject;
    GameObject currentSoundObject;
    
    public bool isRepaired;

    public void HitAction()
    {
        Debug.Log("Object hit");

        ToggleRepair(true);

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource)
        {
            audioSource.Play();
        }
    }

    public void ToggleRepair(bool repaired)
    {
        isRepaired = repaired;
        if (currentSoundObject) Destroy(currentSoundObject);

        if (repaired)
        {
            if (mesh && hitMat)
            {
                mesh.material = hitMat;
            }

            waypoint.SetActive(false);
            particle.Stop();

            currentSoundObject = Instantiate(repairedSoundObject);
        }
        else
        {
            if (mesh && normalMat)
            {
                mesh.material = normalMat;
            }

            waypoint.SetActive(true);
            particle.Play();

            currentSoundObject = Instantiate(brokenSoundObject);
        }

        if (currentSoundObject) 
            currentSoundObject.transform.position = transform.position;
    }
}
