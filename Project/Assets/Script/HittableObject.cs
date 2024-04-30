using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableObject : MonoBehaviour
{
    [SerializeField] Material hitMat;
    [SerializeField] MeshRenderer mesh;
    public void HitAction()
    {
        Debug.Log("Object hit");

        if (mesh && hitMat)
        {
            mesh.material = hitMat;
        }
    }
}
