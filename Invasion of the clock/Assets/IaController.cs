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
    private GameObject goInimigo;
    [SerializeField]private PlayerBehaviour Player;

    [SerializeField] private Transform verificaChao;
    [SerializeField] private float raioVerificaChao;

    [SerializeField] private float speed;

    public bool estaNoChao = false;

    private void Awake()
    {
        rbInimigo = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        estaNoChao = Physics2D.Linecast(transform.position, verificaChao.position, solido);
        movimentaçãoInicial();
    }
    public void movimentaçãoInicial()
    {
        rbInimigo.velocity = new Vector2(speed * Time.deltaTime, rbInimigo.velocity.y);
        if (!verificaChao)
        {
            speed = -1*speed;
        }
    }
}
