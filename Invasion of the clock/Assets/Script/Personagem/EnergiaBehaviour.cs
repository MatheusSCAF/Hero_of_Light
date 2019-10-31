using Battlehub.Dispatcher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergiaBehaviour : MonoBehaviour
{
    public float energMax;
    public float energAtual;
    [SerializeField] private Image barraDeVida;
    [SerializeField] private Collider2D sun;
    [SerializeField] private PlayerBehaviour Player;

    void Start()
    {
        energAtual = energMax;
    }
    void Update()
    {
        if (energAtual > energMax)
        {
            energAtual = energMax;
        }
        //barraDeVida.rectTransform.sizeDelta = new Vector2(energAtual / energMax * 178.28f, 28);
        Dispatcher.Current.BeginInvoke(() =>
        {
            barraDeVida.rectTransform.sizeDelta = new Vector2(energAtual / energMax * 178.28f, 28);
        });
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PerdeEnergy(10f);
        }
    }
    void OnTriggerStay2D(Collider2D sun)
    {
        if (sun.gameObject.tag == "Sun")
        {
            StartCoroutine(ganhaEnerg(20f));
        }

    }
    public void PerdeEnergy(float perdeEnergia)
    {
        energAtual -= perdeEnergia;
    }


    public IEnumerator ganhaEnerg(float acrescentaEnerg)
    {
        energAtual += 0.5f;
        yield return new WaitWhile(() => energAtual < energAtual + acrescentaEnerg);
    }
}
