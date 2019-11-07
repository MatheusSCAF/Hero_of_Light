using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseWallBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private int contador;
    private void OnTriggerEnter2D(Collider2D collider)
    {
            if (collider.gameObject.tag == "Bala")
            {
            Destroy(collider.gameObject);
            contador++;
            Debug.Log(contador);
            if (contador == 5)
            {
                Destroy(this.gameObject);
            }

        }


    }
}


