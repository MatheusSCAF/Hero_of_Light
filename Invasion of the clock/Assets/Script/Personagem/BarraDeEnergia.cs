using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeEnergia : MonoBehaviour
{
    public float energMax;
    public float energAtual;

    [SerializeField] private Image barraDeEnergia;
    [SerializeField] private Collider2D sun;
    [SerializeField] private PlayerBehaviour Player;
    // Start is called before the first frame update
    void Start()
    {
      
        energAtual = energMax;
    }


    // Update is called once per frame
    void Update()
    {
        if (energAtual > energMax)
        {
            energAtual = energMax;
        }
        barraDeEnergia.rectTransform.sizeDelta = new Vector2(energAtual / energMax * 178.28f, 21.06f);

    }
    public IEnumerator perdeEnergia(float decrementoenerg)
    {
       
            energAtual -= 1f;
            yield return new WaitWhile(() => energAtual < energAtual - decrementoenerg);
        
        

    }
    void OnTriggerStay2D(Collider2D sun)
    {
        if (sun.gameObject.tag == "Sun")
        {
            StartCoroutine(ganhaEnerg(2f));
        }


    }
    public IEnumerator ganhaEnerg(float acrescentaEnerg)
    {
        energAtual += 1f;
        yield return new WaitWhile(() => energAtual > energAtual + acrescentaEnerg);

    }

}
