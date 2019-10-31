using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaController : MonoBehaviour
{
    public float vidaMax;
    public float vidaAtual;
    [SerializeField] private Image barraDeVida;
    [SerializeField] private Collider2D sun;
    [SerializeField] private PlayerBehaviour Player;

    void Start()
    {
       vidaAtual = vidaMax;   
    }
    void Update()
    {
        if (vidaAtual > vidaMax)
        {
            vidaAtual = vidaMax;
        }
        barraDeVida.rectTransform.sizeDelta = new Vector2(vidaAtual / vidaMax * 178.28f,28);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            vidaAtual -= 10f;
        }
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
        vidaAtual += 0.5f;
        yield return new WaitWhile(() => vidaAtual < vidaAtual + acrescentaVida);
    }
}
