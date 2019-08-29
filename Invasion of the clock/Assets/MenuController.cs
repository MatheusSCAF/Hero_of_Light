using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //Variaveis
    public GameObject menuUI;
    public GameObject opcoesUI;
    public string nomeCena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void Game()
    {
        SceneManager.LoadScene(nomeCena);
    }
    public void Opcoes()
    {
        opcoesUI.SetActive(true);
        menuUI.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
