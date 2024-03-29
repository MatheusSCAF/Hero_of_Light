﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;

    [SerializeField] GameObject destroyEffect;

    private void Start()
    {
        Invoke("DestroyProjectile",lifeTime);
    }
    private void Update()
    {
      transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    private void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
