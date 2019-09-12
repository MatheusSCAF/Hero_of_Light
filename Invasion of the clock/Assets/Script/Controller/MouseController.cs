using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
<<<<<<< HEAD
    private
   
=======
>>>>>>> ad2f4985cf68daec7895ceaa5194418cf2b3f5e2
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
<<<<<<< HEAD
        Debug.Log(mousePosition.x);
        Debug.Log(mousePosition.y);
        Debug.Log("--");


        transform.right = direcao;

        //Vector3 mouseRotate = new Vector3()
=======


        transform.right  = direcao;

>>>>>>> ad2f4985cf68daec7895ceaa5194418cf2b3f5e2
    }
    
}
