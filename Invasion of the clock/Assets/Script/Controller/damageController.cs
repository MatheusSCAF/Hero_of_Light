using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageController : MonoBehaviour
{
    private healthManaBarController vidaEMana;
    private PlayerBehaviour player;
    private Rigidbody2D rb;
    private Animator an;

    [SerializeField]private float tempoDeInvencibilidade;
    [SerializeField] private float knockback;

    private bool invecibilidade = false;

    void Start()
    {
        vidaEMana = gameObject.GetComponent<healthManaBarController>();
        player = gameObject.GetComponent<PlayerBehaviour>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        an = gameObject.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        an.SetBool("Invencivel", invecibilidade);
    }
    private void KnockBack()
    {
        if (player.viradoParaDireita)
        {
            rb.velocity = new Vector2(knockback, knockback);
        }
        else if (!player.viradoParaDireita)
        {
            rb.velocity = new Vector2(-knockback, knockback);
        }
    }
    private IEnumerator Invencivel()
    {
        invecibilidade = true;
        yield return new WaitForSeconds(tempoDeInvencibilidade);
        invecibilidade = false;
    }
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (!invecibilidade)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                invecibilidade = true;
                StartCoroutine(Invencivel());
                vidaEMana.lostLife(15f);
                KnockBack();
            }
        }
    }
}
