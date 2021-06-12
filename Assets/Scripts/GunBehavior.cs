using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    public Transform muzzleTransform;
    public GameObject projectile;
    public bool canFire = true;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (canFire)
            {
                Instantiate(projectile, muzzleTransform);
                canFire = false;
            }
        }
        else
            canFire = true;
    }
}
