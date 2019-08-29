using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseController : MonoBehaviour
{

    [SerializeField] private static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private string nomeCena;


    void Update()
    {
        //Void de checagem
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
       
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(nomeCena);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
