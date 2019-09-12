using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPf;
<<<<<<< HEAD
    private BarraDeEnergia barraDeEnergia;

    // Update is called once per frame
     void Awake()
    {
        barraDeEnergia = GetComponent<BarraDeEnergia>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            
        }
           

=======

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
>>>>>>> ad2f4985cf68daec7895ceaa5194418cf2b3f5e2
    }
    void Shoot()
    {
        Instantiate(bulletPf, firePoint.position, firePoint.rotation);
<<<<<<< HEAD
        barraDeEnergia.perdeEnergia(1f);
=======
>>>>>>> ad2f4985cf68daec7895ceaa5194418cf2b3f5e2
    }
}
