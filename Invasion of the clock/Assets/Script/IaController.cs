using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class IaController : MonoBehaviour
{
    private enum EnemyTyper{ Petroleo,Carvao,Default}
    [SerializeField] private EnemyTyper inimigos;
    [SerializeField] private LayerMask solido;
    [SerializeField] private LayerMask Player;

    private Rigidbody2D rbInimigo;
    private Transform trInimigo;

    [SerializeField] private Rigidbody2D PlayerRB;
    [SerializeField] private Transform[] campoDeVisao;
    [SerializeField] private Transform PlayerTr;
    [SerializeField] private Transform verificaChao;

    [SerializeField] private float speed;
    [SerializeField] private float minX,maxX;
    private float posInicial;

    public bool[] raioDeEncontro;
    public bool estaVendo = false;
    private bool bounds = true;
    private bool viradoParaDireita = true;
    private bool estaNoChao = false;

    private void Awake()
    {
        rbInimigo = GetComponent<Rigidbody2D>();
        trInimigo = GetComponent<Transform>();
        posInicial = transform.position.x;
        minX = posInicial - minX;
        maxX = posInicial + maxX;
    }
    private void Update()
    {
        estaNoChao = Physics2D.Linecast(transform.position, verificaChao.position, solido);
        raioDeEncontro[0] = Physics2D.Linecast(transform.position, campoDeVisao[0].position,Player);
        raioDeEncontro[1] = Physics2D.Linecast(transform.position, campoDeVisao[1].position, Player);
    }
    private void FixedUpdate()
    {
        if(!estaVendo)
        {
            MovimentaçãoInicial();
        }
        FollowPlayer();
    }
    private void MovimentaçãoInicial()
    {
        rbInimigo.velocity = new Vector2(speed * Time.deltaTime, rbInimigo.velocity.y);
        if (!estaNoChao)
        {
            speed = speed * -1;
            Flip();
        }
        if (trInimigo.position.x < minX && bounds || bounds && trInimigo.position.x > maxX)
        {
            StartCoroutine(Wait());
            speed = speed * -1;
        }
    }
    private void FollowPlayer()
    {
        if (raioDeEncontro[0] && estaNoChao || raioDeEncontro[1] && estaNoChao)
        {
            estaVendo = true;
            if (trInimigo.position.x >PlayerTr.position.x && estaVendo && estaNoChao || trInimigo.position.x < PlayerTr.position.x && estaVendo && estaNoChao)
            {
                trInimigo.position = Vector2.MoveTowards(transform.position, PlayerTr.position, speed * Time.deltaTime);
            }
        }
        else
        {
            estaVendo = false;
        }
    }
    private void Flip()
    {   
        if (speed > 0 && !viradoParaDireita || speed < 0 && viradoParaDireita)
        {
            trInimigo.localScale = new Vector2(-trInimigo.localScale.x, trInimigo.localScale.y);
            viradoParaDireita = !viradoParaDireita;
        }
    }
    private IEnumerator Wait()
    {
        trInimigo.localScale = new Vector2(-trInimigo.localScale.x, trInimigo.localScale.y);
        viradoParaDireita = !viradoParaDireita;
        bounds = false;
        yield return new WaitForSeconds(0.5f);
        bounds = true;
    }
    private IEnumerator WaitF()
    {
        trInimigo.localScale = new Vector2(-trInimigo.localScale.x, trInimigo.localScale.y);
        viradoParaDireita = !viradoParaDireita;
        estaVendo = false;
        yield return new WaitForSeconds(0.5f);
        estaVendo = true;
    }
}
