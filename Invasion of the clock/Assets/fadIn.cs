using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fadIn : MonoBehaviour
{
    public SpriteRenderer render;
    private void Start()
    {
            StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = render.material.color;
            c.a = f;
            render.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene("Menu");
    }
}
