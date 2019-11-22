using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VoltarCena : MonoBehaviour
{
    public Button voltar;
    public string nomeCena;
    public 
    // Start is called before the first frame update

    void Start()
    {
        voltar.onClick.AddListener(() => Voltar());
    }

    public void Voltar()
    {
        SceneManager.LoadScene(nomeCena);
    }
}
