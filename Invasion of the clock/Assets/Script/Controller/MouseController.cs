using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    void Update()
    {
        MouseControler();
    }
    void MouseControler()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direcao = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );


        transform.right  = direcao;

    }
    
}
