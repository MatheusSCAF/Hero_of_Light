using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPf;
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



        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
                Shoot();
        }
        void Shoot()
        {
            Instantiate(bulletPf, firePoint.position, firePoint.rotation);
            barraDeEnergia.perdeEnergia(1f);
        }
    }
}
