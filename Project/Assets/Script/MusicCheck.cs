using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCheck : MonoBehaviour
{
    Climber player;
    MusicManager manager;
    SphereCollider sphereCollider;
    bool inSphere;

    private void Start()
    {
        player = FindObjectOfType<Climber>();
        manager = FindObjectOfType<MusicManager>();
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= sphereCollider.radius)
        {
            if (!inSphere)
            {
                inSphere = true;
                manager.FadeBetweenSongs(true);
            }
        }
        else
        {
            if (inSphere)
            {
                inSphere = false;
                manager.FadeBetweenSongs(false);
            }
        }
    }
}
