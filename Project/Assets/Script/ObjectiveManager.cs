using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] HittableObject[] reactors;
    [SerializeField] int reactorIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        reactors = FindObjectsOfType<HittableObject>();

        for (int i = 0; i < reactors.Length; i++)
        {
            if (i != 0) reactors[i].ToggleRepair(true);
            else reactors[i].ToggleRepair(false);
        }
    }

    private void Update()
    {
        NextObjective();
    }

    void NextObjective()
    {
        if (reactors[reactorIndex].isRepaired)
        {
            reactorIndex++;
            if (reactorIndex >= reactors.Length)
            {
                reactorIndex = 0;
            }

            reactors[reactorIndex].ToggleRepair(false);
        }
    }
}
