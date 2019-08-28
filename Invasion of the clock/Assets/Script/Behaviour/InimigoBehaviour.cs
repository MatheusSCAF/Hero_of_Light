using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoBehaviour : MonoBehaviour
{
    private Transform tr;

    public float speed;
    public float minX;
    public float maxX;

    void Start()
    {
        tr = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (tr.position.x < minX || tr.position.x > maxX)
        {
            Flip();
        }
    }
    public void Flip()
    {
        tr.localScale = new Vector2(-tr.localScale.x, tr.localScale.y);
        speed = speed * -1;
    }
}

