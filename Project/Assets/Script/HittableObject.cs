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

    [SerializeField] GameObject waypoint;
    [SerializeField] ParticleSystem particle;

    [SerializeField] GameObject brokenSoundObject;
    [SerializeField] GameObject repairedSoundObject;
    [SerializeField] GameObject currentSoundObject;
    
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
        if (currentSoundObject)
        {
            currentSoundObject.GetComponent<AkAmbient>().Stop(1);
            Destroy(currentSoundObject);
        }

        if (repaired)
        {
            waypoint.SetActive(false);
            particle.Stop();

            currentSoundObject = Instantiate(repairedSoundObject);
        }
        else
        {
            waypoint.SetActive(true);
            particle.Play();

            currentSoundObject = Instantiate(brokenSoundObject);
        }

        if (currentSoundObject) 
            currentSoundObject.transform.position = transform.position;
    }
}
