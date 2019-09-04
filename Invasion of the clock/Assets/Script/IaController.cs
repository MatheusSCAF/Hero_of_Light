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

    private Rigidbody2D rbInimigo;
    private Transform trInimigo;
    private GameObject goInimigo;
    [SerializeField]private PlayerBehaviour Player;

    [SerializeField] private Transform verificaChao;

    [SerializeField] private float speed;
    [SerializeField] private float minX,maxX;
    private float posInicial;

    [SerializeField] private bool bounds = false;
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
        movimentaçãoInicial();
    }
    public void movimentaçãoInicial()
    {
        if (!estaNoChao)
        {
            speed = speed * -1;
            flip();
        }
        //bounds em andamento não ative ainda
        else if (trInimigo.position.x > minX && bounds || trInimigo.position.x < maxX && bounds)
        {
           speed = speed * -1;
        }
        rbInimigo.velocity = new Vector2(speed * Time.deltaTime, rbInimigo.velocity.y);
    }
    public void flip()
    {
        if (speed > 0 && !viradoParaDireita || speed < 0 && viradoParaDireita)
        {
            trInimigo.localScale = new Vector2(-trInimigo.localScale.x, trInimigo.localScale.y);
            viradoParaDireita = !viradoParaDireita;
        }
    }
}
