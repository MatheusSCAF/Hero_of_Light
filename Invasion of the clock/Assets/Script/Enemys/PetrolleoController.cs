using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolleoController : MonoBehaviour
{
    [SerializeField] private LayerMask solido;
    [SerializeField] private Transform verificaChao;

    private Rigidbody2D rbInimigo;

    [SerializeField] private float speed,damage;
    [SerializeField] private float minX, maxX;

    [SerializeField] private bool bounds = true;
    [SerializeField] private bool invecibilidade;
    private bool viradoParaDireita = true;
    private bool estaNoChao = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bala")
        {
            damage = -1;
        }  
    }
    private void Flip()
    {
        if (speed > 0 && !viradoParaDireita || speed < 0 && viradoParaDireita)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            viradoParaDireita = !viradoParaDireita;
        }
    }
    private void Awake()
    {
        rbInimigo = GetComponent<Rigidbody2D>();
        minX = transform.position.x - minX;
        maxX = transform.position.x + maxX;
    }
    private void Update()
    {
            estaNoChao = Physics2D.Linecast(transform.position, verificaChao.position, solido);
    }
    private IEnumerator Wait()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        viradoParaDireita = !viradoParaDireita;
        bounds = false;
        yield return new WaitForSeconds(0.5f);
        bounds = true;
    }
    private void FixedUpdate()
    {
            MovimentaçãoTerreste();
        if (damage <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void MovimentaçãoTerreste()
    {
        rbInimigo.velocity = new Vector2(speed, rbInimigo.velocity.y);
        if (!estaNoChao)
        {
            speed = speed * -1;
            Flip();
        }
        if (transform.position.x < minX && bounds || bounds && transform.position.x > maxX)
        {
            StartCoroutine(Wait());
            speed = speed * -1;
        }
    }
}
