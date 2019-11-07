using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPf;
    [SerializeField] healthManaBarController hmc;
    // Update is called once per frame
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            hmc.shoot = true;
        }
    }
    void Shoot()
    {
        Instantiate(bulletPf, firePoint.position, firePoint.rotation);
    }
}
