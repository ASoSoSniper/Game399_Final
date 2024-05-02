using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicTrack;
   
    //when you enter box music should play :3

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AMONGUS");
        audioSource = other.gameObject.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(musicTrack);
    }
}
