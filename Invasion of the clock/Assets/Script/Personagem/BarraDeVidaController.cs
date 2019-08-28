using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaController : MonoBehaviour
{
    public Image barraDeVida;
    public Collider2D sun;
    
    public PlayerBehaviour Player;

    void Start()
    {
        Player.vidaAtual = Player.vidaMax;   
    }
    void Update()
    {
        if (Player.vidaAtual > Player.vidaMax)
        {
            Player.vidaAtual = Player.vidaMax;
        }
        barraDeVida.rectTransform.sizeDelta = new Vector2(Player.vidaAtual / Player.vidaMax * 178.28f,28);

    }
    public IEnumerator perdevida(float decrementodavida)
    {
        Player.vidaAtual -= 4f;
        yield return new WaitWhile(() => Player.vidaAtual < Player.vidaAtual - decrementodavida);

    }
    void OnTriggerStay2D(Collider2D sun)
    {
        if (sun.gameObject.tag == "Sun")
        {
            StartCoroutine(ganhaVida(20f));
        }
       
    }

    public IEnumerator ganhaVida(float acrescentaVida)
    {
        Player.vidaAtual += 1f;
        yield return new WaitWhile(() => Player.vidaAtual > Player.vidaAtual + acrescentaVida);

    }
}
