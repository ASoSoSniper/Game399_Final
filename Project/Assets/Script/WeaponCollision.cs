using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [SerializeField] MeleeWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<MeleeWeapon>();
        if (!weapon) weapon = GetComponentInParent<MeleeWeapon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!weapon) return;

        if (weapon.spaceRotation.magnitude >= weapon.hitRotation)
        {
            weapon.HitObject(other);
        }
    }
}
