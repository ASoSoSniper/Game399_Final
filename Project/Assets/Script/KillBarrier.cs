using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBarrier : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Climber>())
        {
            FindObjectOfType<OxygenFunctionality>().KillPlayer();
            Debug.Log("Out of bounds");
        }
    }
}
