using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatafBehaviour : MonoBehaviour
{
    private float posIncial;
    [SerializeField]private float maxX,minX,speed = 10;
    private Rigidbody2D rB;

    void Awake()
    {
        posIncial = transform.position.x;
        maxX = posIncial + maxX;
        minX = posIncial - minX;
    }
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.x > maxX || transform.position.x < minX)
        {
            speed *= -1;
        }
    }
}
