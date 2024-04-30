using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTank : MonoBehaviour
{
    public float oxygenQuantity = 10f;
    public float destructionTime = 0.5f;
    bool destroying = false;

    public void GrabOxygen()
    {
        if (!destroying)
        {
            OxygenFunctionality oxygenTarget = FindObjectOfType<OxygenFunctionality>();
            if (!oxygenTarget) return;

            oxygenTarget.GainOxygen(oxygenQuantity);

            Destroy(gameObject, destructionTime);
            destroying = true;
        }
    }
}
