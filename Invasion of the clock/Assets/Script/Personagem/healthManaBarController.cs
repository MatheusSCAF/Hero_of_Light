using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class healthManaBarController : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;

    private float maxHealth, maxMana;
    private float health = 100, mana = 100;
    private float fillHealth, fillMana;

    public bool takeDamage;
    public bool gameOver;
    public bool shoot;
    public bool dash;
 

    void Awake()
    {
        maxHealth = health;
        maxMana = mana;
    }
    void FixedUpdate()
    {
        
        Converter();
        Activador();
    }   
    void Converter()
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        else if (mana >= maxMana)
        {
            mana = maxMana;
        }

        fillHealth = health / maxHealth;
        fillMana = mana / maxMana;

        healthBar.fillAmount = fillHealth;
        manaBar.fillAmount = fillMana;
    }
    void Activador()
    {
        if (shoot)
        {
            if (mana < 0)
            {
                lostLife(20);
                if (health < 0)
                {
                    gameOver = true;
                }
            }
            lostMana(10f);
            shoot = false;
        }
        if (dash)
        {
            if (mana < 0)
            {
                lostLife(25);
                if (health < 0)
                {
                    gameOver = true;
                }
            }
            lostMana(30);
            dash = false;
        }
        if (takeDamage)
        {
            if (health < 0)
            {
                gameOver = true;
            }
            lostLife(20);
            takeDamage = false;
        }
    }
    public void lostLife(float lifeLost)
    {
        health -= lifeLost;
    }
    public void lostMana(float manaLost)
    {
        mana -= manaLost;
    }
    public IEnumerator recoverLife(float lifeRecover)
    {
        health += 2f;
        yield return new WaitWhile(() => health < health + lifeRecover);
    }
    public IEnumerator recoverMana(float manaRecover)
    {
        mana += 2f;
        yield return new WaitWhile(() => mana < mana + manaRecover);
    }

    void OnTriggerStay2D(Collider2D sun)
    {
        if (sun.gameObject.tag == "Sun")
        {
            if (health <= maxHealth)
            {
                StartCoroutine(recoverLife(10f));
            }
            if (mana <= maxMana)
            {
                StartCoroutine(recoverMana(10f));
            }
        }
    }
}
