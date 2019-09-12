using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(TalkActivationController))]
[RequireComponent(typeof(BarraDeVidaController))]
public class PlayerBehaviour : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D rb;
    private Animator an;
    private SpriteRenderer sr;
    private TalkActivationController playerTalk;
    private BarraDeVidaController barraDeVida;
    [SerializeField] private PowerUpBehaviour[] powerUp;
    [SerializeField] private GameObject pUp;
    [SerializeField] private Transform verificaChao;
    [SerializeField] private Transform verificaParede;
    [SerializeField] private Transform verificaEscada;
    [SerializeField] private Transform verificaAreaDaInteraçao;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject canonB;
    [SerializeField] private LayerMask solido;
    [SerializeField] private LayerMask plataformaEspeciais;
    [SerializeField] private LayerMask interaçãoDoPlayer;

    private bool estaVivo = true;
    private bool estaAndando = false;
    private bool estaNoChao = false;
    private bool estaNaParede = false;
    private bool estaNaEscada = false;
    private bool estaPulando = false;
    private bool estaNoRaioDaInteraçao = false;
    private bool subindoNaEscada = false;
    private bool viradoParaDireita = true;
    private bool estaNoAr;
    public bool puloDuplo;
    public bool wallJump;
    public bool canonBlaster;

    [SerializeField] private Vector2 wJump;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float velocidadeDeSubida;
    [SerializeField] private float velocidadeDeDecida;
    [SerializeField] private float raioVerificaChao;
    [SerializeField] private float raioVerificaParede;
    [SerializeField] private float raioVerificaEscada;
    [SerializeField] private float raioVerificaAreaDaInteração;
    [SerializeField] private float x,y,z;
    private float axis;
    private int pulosExtras = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
        playerTalk = GetComponent<TalkActivationController>();
        barraDeVida = GetComponent<BarraDeVidaController>();
    }
    void Update()
    {
        //checagem se esta vivo

        if (barraDeVida.vidaAtual == 0)
        {
            estaVivo = false;
        }

        //Checagem se esta no chao ou na parede ou ta na escada ou se ta numa area de interação do player

        estaNoChao = Physics2D.OverlapCircle(verificaChao.position,raioVerificaChao,solido);
        estaNaParede = Physics2D.Linecast(transform.position,verificaParede.position,solido);
        estaNaEscada = Physics2D.OverlapCircle(verificaEscada.position,raioVerificaEscada,plataformaEspeciais);
        estaNoRaioDaInteraçao = Physics2D.OverlapCircle(verificaAreaDaInteraçao.position,raioVerificaAreaDaInteração,interaçãoDoPlayer);
        if (!estaNoAr & estaNoChao)
        {
            estaNoAr = true;
        }
        //Checagem se esta pulando
        if (!puloDuplo)
        {
            if (Input.GetKeyDown(KeyCode.Space) && pulosExtras >= 0)
            {
                estaPulando = true;
                pulosExtras--;
            }
            if (estaNoChao)
            {
                pulosExtras = 0;
            }
            else if (!estaNoChao)
            {
                pulosExtras =-1;
            }
        }
        else if (puloDuplo)
        {
            if (Input.GetKeyDown(KeyCode.Space) && pulosExtras >= 0)
            {
                estaPulando = true;
                pulosExtras--;
            }
            else if (estaNoChao)
            {
                pulosExtras = 1;
            }
            else if (!estaNoChao && estaNoAr)
            {
                pulosExtras = 0;
                estaNoAr = false;
            }
            else if (!estaNoChao && estaPulando)
            {
                pulosExtras = -1;
            }
            }
        //Checagem se vai subir na escada

        if (estaNaEscada && Input.GetKey(KeyCode.W) || estaNaEscada && Input.GetKey(KeyCode.S))
        {
            subindoNaEscada = true;
        }
        
    }
    void FixedUpdate()
    {
        if(estaVivo)
        {
            moviment();
            puloNaParede();
            Escada();
        }    
    }
    void OnDrawGizmosSelected()
    {
        //desenhos de esfera no unity 
        //ps:não faz parte da programação em si.

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(verificaChao.position,raioVerificaChao);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(verificaParede.position,raioVerificaParede);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(verificaEscada.position,raioVerificaEscada);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(verificaAreaDaInteraçao.position,raioVerificaAreaDaInteração);

    }
    void OnTriggerStay2D(Collider2D col)
    {
            interação(col);
    }
    void moviment()
    {

        //Andar

        axis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(axis * speed *Time.deltaTime, rb.velocity.y);
        estaAndando = Mathf.Abs(axis) > 0;

        if (axis > 0 && !viradoParaDireita || axis < 0 && viradoParaDireita)
        {
            Flip();
        }
        if (estaAndando && !estaNaParede)
        {
            if (viradoParaDireita)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
        
        //Pulo

        if (estaPulando)
        {
            rb.velocity = new Vector2(rb.velocity.x,0);
            rb.AddForce(new Vector2(0f,jumpForce*Time.deltaTime));
            estaPulando = false;
        }
        if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * -0.8f;
        }
    }
    void Escada()
    {
        //Subir na escada

        if (subindoNaEscada)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //rb.simulated = true;
                rb.velocity = new Vector2(rb.velocity.x,velocidadeDeSubida);
            }
            if (Input.GetKey(KeyCode.S))
            {
               //rb.simulated = true;
                rb.velocity = new Vector2(rb.velocity.x, velocidadeDeDecida);
            }
            if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                //rb.simulated = false;
            }
            if (estaNaEscada == false)
            {
                subindoNaEscada = false;
            }
        }
    }
    void interação(Collider2D col)
    { 
        if (!estaAndando && estaNoRaioDaInteraçao && estaNoChao)
        {
                playerTalk.setaAparecer();
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (col.gameObject.tag == "pD")
                {
                    if (powerUp[0].powerUps == PowerUpsController.PuloDuplo)
                    {
                        pulosExtras = 1;
                        puloDuplo = true;
                    }
                    else if (powerUp[0].powerUps == PowerUpsController.CanomBlaster)
                    {
                        canonB.SetActive(true);
                        canonBlaster = true;
                    }
                    else if (powerUp[0].powerUps == PowerUpsController.CanomBlaster)
                    {
                        wallJump = true;
                    }
                    powerUp[0].Destroir();
                }
                if (col.gameObject.tag == "wJ")
                {
                    if (powerUp[1].powerUps == PowerUpsController.PuloDuplo)
                    {
                        pulosExtras = 1;
                        puloDuplo = true;
                    }
                    else if (powerUp[1].powerUps == PowerUpsController.CanomBlaster)
                    {
                        canonB.SetActive(true);
                        canonBlaster = true;
                    }
                    else if (powerUp[1].powerUps == PowerUpsController.CanomBlaster)
                    {
                        wallJump = true;
                    }
                    powerUp[1].Destroir();
                }
                if (col.gameObject.tag == "cB")
                {
                    if (powerUp[3].powerUps == PowerUpsController.PuloDuplo)
                    {
                        pulosExtras = 1;
                        puloDuplo = true;
                    }
                    else if (powerUp[3].powerUps == PowerUpsController.CanomBlaster)
                    {
                        canonB.SetActive(true);
                        canonBlaster = true;
                    }
                    else if (powerUp[3].powerUps == PowerUpsController.CanomBlaster)
                    {
                        wallJump = true;
                    }
                    powerUp[3].Destroir();
                }
            }
        }
            else if (estaAndando || !estaNoRaioDaInteraçao || !estaNoChao || !estaNoChao && !estaNoRaioDaInteraçao || estaAndando && !estaNoRaioDaInteraçao || estaAndando && !estaNoRaioDaInteraçao && !estaNoChao)
            {
                playerTalk.setaDesaparecer();
            }
    }
    void puloNaParede()
    {
        if (estaNaParede && !estaNoChao && wallJump)
        {
            pulosExtras = -1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(wJump.x*-axis * Time.deltaTime, wJump.y * Time.deltaTime);
                pulosExtras = 0;
            }
        }
    }
    void Flip()
    {
        tr.localScale = new Vector2(-tr.localScale.x,tr.localScale.y);
        //firePoint.localScale = new Vector2(-tr.localScale.x, -tr.localScale.y);
        viradoParaDireita = !viradoParaDireita;
        //sr.flipX = !sr.flipX;
    }    
}