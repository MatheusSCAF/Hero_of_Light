using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageController : MonoBehaviour
{
    private healthManaBarController vidaEMana;
    private Rigidbody2D rb;
    void Start()
    {
        vidaEMana = gameObject.GetComponent<healthManaBarController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void knockback(Vector2 vetor)
    {
        rb.AddForce(vetor);
        vidaEMana.lostLife(10f);
    }
}
