using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    private enum EnemyType { Voador, Terrestre };
    [SerializeField] private Vector2 force;
    [SerializeField] private LayerMask solido;
    [SerializeField] private LayerMask Player;
    [SerializeField] private Transform verificaChao;
    [SerializeField] private Transform posicaoInicial;
    [SerializeField] private Transform regiaoDeAtaque;
    private Transform alvoTr;

    private damageController dano;

    private Rigidbody2D rbInimigo;

    [SerializeField] private float speed;
    [SerializeField] private float minX, maxX;
    [SerializeField] private float raioDaAreaDeAtaque;
    [SerializeField] private float distanciaDeFreio, MaxDistancia;

    [SerializeField] private bool encontro = true;
    [SerializeField] private bool bounds = true;
    private bool viradoParaDireita = true;
    private bool startPosision = false;
    private bool areaDeAtaque = false;
    private bool estaNoChao = false;
    private bool invencible = false;
    private bool chao = true;

    [SerializeField] private Transform[] campoDeVisao;
    [SerializeField] private bool[] areaCV;

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
        dano = GameObject.FindGameObjectWithTag("Player").GetComponent<damageController>();
        alvoTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rbInimigo = GetComponent<Rigidbody2D>();
        minX = posicaoInicial.position.x - minX;
        maxX = posicaoInicial.position.x + maxX;
    }
    private void Update()
    {
        areaCV[0] = Physics2D.Linecast(transform.position, campoDeVisao[0].position, Player);
        areaCV[1] = Physics2D.Linecast(transform.position, campoDeVisao[1].position, Player);
        areaDeAtaque = Physics2D.OverlapCircle(regiaoDeAtaque.position, raioDaAreaDeAtaque, Player);

        if (chao)
        {
            estaNoChao = Physics2D.Linecast(transform.position, verificaChao.position, solido);
        }
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
        if (!encontro)
        {
            MovimentaçãoTerreste();
        }
        if (encontro)
        {
            MovimentaçãoAoVerOPlayer();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(regiaoDeAtaque.position, raioDaAreaDeAtaque);
    }
    public void Encounter(bool sP)
    {
        startPosision = sP;
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
        else if (areaCV[0] & estaNoChao|| areaCV [1] & estaNoChao)
        {
            encontro = !encontro;
            bounds = false;
        }
    }
    private void MovimentaçãoAoVerOPlayer()
    {
        Vector2 vetor;
        bool area = false;

        if (areaCV[0] & estaNoChao & !areaDeAtaque)
        {
            area = true;
        }
        if (areaCV[1] & estaNoChao & !areaDeAtaque)
        {
            speed *= -1;
            Flip();
        }
        if (!areaCV[0] & estaNoChao & !areaDeAtaque || !areaCV[1] & estaNoChao & !areaDeAtaque)
        {
            area = false;            
        }
        if (startPosision)
        {
            encontro = !encontro;
            bounds = true;
        }
        if (invencible & estaNoChao)
        {
            rbInimigo.velocity = new Vector2(speed, rbInimigo.velocity.y);
            if (viradoParaDireita & transform.position.x > posicaoInicial.position.x || !viradoParaDireita & transform.position.x < posicaoInicial.position.x)
            {
                speed *= -1;
                Flip();
            }
        }
        if (areaDeAtaque & areaCV[1])
        {
            dano.knockback(force);
            StartCoroutine(Invecibilidade());
        }
        if (area)
        {
            vetor = Vector2.MoveTowards(transform.position, alvoTr.position, Mathf.Abs(speed) * Time.deltaTime * 1.5f);
            transform.position = new Vector2(vetor.x, transform.position.y);
        }
    }
    private IEnumerator Invecibilidade()
    {
        areaDeAtaque = true;
        yield return new WaitForSeconds(2f);
        areaDeAtaque = false;
    }
}
