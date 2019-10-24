using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class IaController : MonoBehaviour
{
    private enum EnemyType{Voador,Terrestre};
    [SerializeField] private LayerMask solido;
    [SerializeField] private LayerMask Player;
    private Vector3 velocity;

    private Rigidbody2D rbInimigo;
    private Transform trInimigo;

    [SerializeField] private Transform verificaChao;
    [SerializeField] private Transform areaDeAtaqueTerrestre;
    [SerializeField] private Transform PlayerTransform;


    [SerializeField] private float speed;
    [SerializeField] private float minX,maxX;
    private float posInicial;
    [SerializeField]
    private float delay;
    public float raioVerificaChao;

    [SerializeField] private bool bounds = true;
    private bool atacar = true;
    private bool viradoParaDireita = true;
    private bool raioDeAtaque = false;
    [SerializeField] private bool estaNoChao = false;

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
        estaNoChao = Physics2D.Linecast(transform.position,verificaChao.position,solido);
        raioDeAtaque = Physics2D.Linecast(transform.position, areaDeAtaqueTerrestre.position, Player);
    }
    private void FixedUpdate()
    {
        MovimentaçãoTerreste();
        AtaqueAoPlayerTerrestre();
    }
    private void MovimentaçãoTerreste()
    {
        rbInimigo.velocity = new Vector2(speed, rbInimigo.velocity.y);
        if (!estaNoChao)
        {
            speed = speed * -1;
            Flip();
        }
        if (trInimigo.position.x < minX && bounds || bounds && trInimigo.position.x > maxX)
        {
            Flip();
            StartCoroutine(Wait());
            speed = speed * -1;
        }
    }
    private void AtaqueAoPlayerTerrestre()
    {
            if (raioDeAtaque)
            {
                if (atacar)
                {
                    Debug.Log("atacarrr cornos");
                    StartCoroutine(Ataque());
                }
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
        bounds = false;
        yield return new WaitForSeconds(0.5f);
        bounds = true;
    }
    private IEnumerator Ataque()
    {
        atacar = false;
        yield return new WaitForSeconds(0.1f);
        atacar = true;
    }
}
