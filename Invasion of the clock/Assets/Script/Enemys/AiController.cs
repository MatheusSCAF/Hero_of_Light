using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    private enum EnemyType { Voador, Terrestre };
    [SerializeField] private LayerMask solido;
    [SerializeField] private LayerMask Player;
    [SerializeField] private Transform verificaChao;
    [SerializeField] private Transform posicaoInicial;

    private Rigidbody2D rbInimigo;

    [SerializeField] private float speed;
    [SerializeField] private float distanciaDeFreio,MaxDistancia;
    [SerializeField] private float minX, maxX;

    private Transform alvo;

    [SerializeField] private bool encontro = true;
    [SerializeField] private bool bounds = true;
    private bool viradoParaDireita = true;
    private bool estaNoChao = false;
    private bool startPosision = false;

    [SerializeField] private Transform[] campoDeVisao;
    [SerializeField] private bool[] areaCV;

    private void Awake()
    {
        alvo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rbInimigo = GetComponent<Rigidbody2D>();
        minX = posicaoInicial.position.x - minX;
        maxX = posicaoInicial.position.x + maxX;
    }
    private void Update()
    {
        estaNoChao = Physics2D.Linecast(transform.position, verificaChao.position, solido);
        areaCV[0] = Physics2D.Linecast(transform.position, campoDeVisao[0].position, Player);
        areaCV[1] = Physics2D.Linecast(transform.position, campoDeVisao[1].position, Player);
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
        else if (areaCV[0] || areaCV [1])
        {
            encontro = !encontro;
            bounds = false;
        }
    }
    private void MovimentaçãoAoVerOPlayer()
    {
        Vector2 vetor;

        if (areaCV[0] & estaNoChao || areaCV[1] & estaNoChao)
        {
            vetor = Vector2.MoveTowards(transform.position, alvo.position, speed * Time.deltaTime);
            transform.position = new Vector2(vetor.x, transform.position.y);
        }
        else if (!estaNoChao || !areaCV[0] || !areaCV[1])
        {
            vetor = Vector2.MoveTowards(transform.position, posicaoInicial.position, speed * Time.deltaTime);
            transform.position = new Vector2(vetor.x,transform.position.y);
            if (startPosision)
            {
                encontro = !encontro;
                bounds = true;
            }
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
    private void Flip()
    {
        if (speed > 0 && !viradoParaDireita || speed < 0 && viradoParaDireita)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            viradoParaDireita = !viradoParaDireita;
        }
    }
    public void Encounter(bool sP)
    {
        startPosision = sP;
    }
}
