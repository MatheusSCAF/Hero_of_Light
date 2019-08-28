using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
   
    // Start is called before the first frame update


    // Update is called once per frame
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
