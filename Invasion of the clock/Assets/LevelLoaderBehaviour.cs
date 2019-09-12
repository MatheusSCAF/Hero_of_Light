using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoaderBehaviour : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public void LoadLevel(string nameScene)
    {
        StartCoroutine(LoadAsynchronously(nameScene));

    }
    IEnumerator LoadAsynchronously(string nameScene )
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f* Time.deltaTime);
            slider.value = progress;
           
            yield return null;
        }
    }
    // Start is called before the first frame update
   
}
