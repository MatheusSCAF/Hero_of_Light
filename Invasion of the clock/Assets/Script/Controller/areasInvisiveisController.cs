using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areasInvisiveisController : MonoBehaviour
{
    SpriteRenderer render;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(FadeOut());
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(FadeIn());
        }
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
    }
    IEnumerator FadeIn()
    {
        for (float f = 0.05f; f <= 1f; f += 0.05f)
        {
            Color c = render.material.color;
            c.a = f;
            render.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
