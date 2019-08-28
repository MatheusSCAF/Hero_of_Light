using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPf;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }
    void Shoot()
    {
        Instantiate(bulletPf, firePoint.position, firePoint.rotation);
    }
}
