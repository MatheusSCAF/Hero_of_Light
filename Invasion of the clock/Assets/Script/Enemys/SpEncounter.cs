using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpEncounter : MonoBehaviour
{
    private AiController ai;
    private bool estaNaPoscicaoInicial;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            ai = collision.GetComponent<AiController>();
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        estaNaPoscicaoInicial = true;
        ai.Encounter(estaNaPoscicaoInicial);
        yield return new WaitForSeconds(0.5f);
        estaNaPoscicaoInicial = false;
    }
}
