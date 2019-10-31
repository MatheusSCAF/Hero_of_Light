using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPf;

    // Update is called once per frame
    EnergiaBehaviour eb;
    void Awake()
    {
        eb = GameObject.FindGameObjectWithTag("Player").GetComponent<EnergiaBehaviour>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();

    }
    void Shoot()
    {
        Instantiate(bulletPf, firePoint.position, firePoint.rotation);
        eb.PerdeEnergy(20f);
    }
}
