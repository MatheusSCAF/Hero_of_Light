using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBehaviour : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(destroir());
    }
    IEnumerator destroir()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
