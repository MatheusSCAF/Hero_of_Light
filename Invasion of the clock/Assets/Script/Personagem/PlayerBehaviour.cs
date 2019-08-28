using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D rb;
    private Animator an;
    private SpriteRenderer sr;
    private TalkActivationController playerTalk;
    public PowerUpBehaviour powerUp;
    public Transform verificaChao;
    public Transform verificaParede;
    public Transform verificaEscada;
    public Transform verificaAreaDaInteraçao;
    public Transform firePoint;
    public GameObject canonB;
    public LayerMask solido;
    public LayerMask plataformaEspeciais;
    public LayerMask interaçãoDoPlayer;


    private bool estaVivo = true;
    private bool estaAndando = false;
    private bool estaNoChao = false;
    private bool estaNaParede = false;
    private bool estaNaEscada = false;
    private bool estaPulando = false;
    private bool estaNoRaioDaInteraçao = false;
    private bool subindoNaEscada = false;
    private bool viradoParaDireita = true;
    public bool puloDuplo;
    public bool wallJump;
    public bool canonBlaster;

    public float speed;
    public float jumpForce;
    public float velocidadeDeSubida;
    public float velocidadeDeDecida;
    public float raioVerificaChao;
    public float raioVerificaParede;
    public float raioVerificaEscada;
    public float raioVerificaAreaDaInteração;
    public float vidaMax;
    public float vidaAtual;

    private float axis;
    public int pulosExtras = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
        playerTalk = GetComponent<TalkActivationController>();
    }
    void Update()
    {
        //checagem se esta vivo

        if (vidaAtual == 0)
        {
            estaVivo = false;
        }

        //Checagem se esta no chao ou na parede ou ta na escada ou se ta numa area de interação do player

        estaNoChao = Physics2D.OverlapCircle(verificaChao.position,raioVerificaChao,solido);
        estaNaParede = Physics2D.OverlapCircle(verificaParede.position,raioVerificaParede,solido);
        estaNaEscada = Physics2D.OverlapCircle(verificaEscada.position,raioVerificaEscada,plataformaEspeciais);
        estaNoRaioDaInteraçao = Physics2D.OverlapCircle(verificaAreaDaInteraçao.position,raioVerificaAreaDaInteração,interaçãoDoPlayer);

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
            else if (!estaNoChao && estaPulando)
            {
                pulosExtras = -1 ;
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
            interação();
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
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Inimigo")
        {
            vidaAtual = vidaAtual - 10;
        }
        if(col.gameObject.tag == "PowerUp")
        {
           // col.gameObject = powerUp.gameObject;
        }
    }
    void moviment()
    {

        //Andar

        axis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(axis * speed , rb.velocity.y);
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
            rb.AddForce(new Vector2(0f,jumpForce));
            estaPulando = false;
        }
        if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * -0.8f;
        }

        //Subir na escada

        if (subindoNaEscada)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(rb.velocity.x,velocidadeDeSubida);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(rb.velocity.x,velocidadeDeDecida);
            }
            if (!Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.W))
            {
                
            }
            if (estaNaEscada == false)
            {
                subindoNaEscada = false;

            }
            /*if (estaNaEscada)
            {
                pulosExtras = 0;
            }*/
        }

    }
    void interação()
    { 
        if (!estaAndando && estaNoRaioDaInteraçao && estaNoChao)
        {
                playerTalk.setaAparecer();
            if (Input.GetKeyDown(KeyCode.W))
            {
                switch (powerUp.powerUps)
                {
                    case PowerUpsController.PuloDuplo:
                        pulosExtras = 1;
                        puloDuplo = true;
                        break;
                    case PowerUpsController.CanomBlaster:
                        canonB.SetActive(true);
                        canonBlaster = true;
                        break; 
                            case PowerUpsController.WallJump:
                                
                        break; 
                    case PowerUpsController.Default:
                        break;
                }
                Destroy(powerUp.gameObject);
            }  
        }
            else if (estaAndando || !estaNoRaioDaInteraçao || !estaNoChao || !estaNoChao && !estaNoRaioDaInteraçao || estaAndando && !estaNoRaioDaInteraçao || estaAndando && !estaNoRaioDaInteraçao && !estaNoChao)
            {
                playerTalk.setaDesaparecer();
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