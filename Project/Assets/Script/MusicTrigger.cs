using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicTrack;
    public Toggle Toggle;
   
    //when you enter box music should play :3

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AMONGUS");
        audioSource = other.gameObject.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(musicTrack);
    }

    private void Update()
    {
        audioSource.loop = Toggle.isOn;
    }
}
