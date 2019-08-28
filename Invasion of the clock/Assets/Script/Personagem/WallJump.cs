using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    public float distance;
    public float speed;
    private PlayerBehaviour moviment;
    void Awake()
    {
        moviment = GetComponent<PlayerBehaviour>();
    }
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right*transform.localScale.x,distance);
        if (Input.GetKeyDown(KeyCode.Space) && !moviment.verificaChao && hit.collider!=null)
        {
            {GetComponent<Rigidbody2D>().velocity = new Vector2(speed * hit.normal.x, speed);
                moviment.speed = speed * hit.normal.x;
                transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
            }
        }
    }

    void OnDrawGizmos()
    {
        //desenhos de esfera no unity 
        //ps:não faz parte da programação em si.

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
