using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private
   
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
        Debug.Log(mousePosition.x);
        Debug.Log(mousePosition.y);
        Debug.Log("--");


        transform.right = direcao;

        //Vector3 mouseRotate = new Vector3()
    }
    
}
