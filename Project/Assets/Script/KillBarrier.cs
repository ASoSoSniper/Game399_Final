using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBarrier : MonoBehaviour
{
    Climber player;
    SphereCollider collider;

    private void Start()
    {
        player = FindObjectOfType<Climber>();

        collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist > collider.radius)
        {
            FindObjectOfType<OxygenFunctionality>().KillPlayer();
            Debug.Log("Out of bounds");
        }
    }
}
