using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private Rigidbody2D rb;
    private PlayerBehaviour pH;
    
    void Start()
    {
        pH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        if (pH.viradoParaDireita)
        {
            speed = Mathf.Abs(speed);
        }
        else if (!pH.viradoParaDireita)
        {
            speed *= -1;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
    void Update()
    {
        rb.velocity = transform.right * speed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        Destroy(gameObject);
    }


}
