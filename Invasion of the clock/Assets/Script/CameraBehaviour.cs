using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform player;
    public PlayerBehaviour playerBh;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
    private Vector2 velocity;

    public float delayX;
    public float delayY;
    public float y,yAtual;

    public bool bounds;

    void Start()
    {
        yAtual = y;
    }
    void FixedUpdate()
    {
        /*if (Input.GetKey(KeyCode.DownArrow) && !playerBh.subindoNaEscada || Input.GetKeyDown(KeyCode.DownArrow) && !playerBh.subindoNaEscada)
        {
            yAtual = 0;
        }
        /* 
        /*if (Input.GetKeyUp(KeyCode.DownArrow) && !playerBh.subindoNaEscada)
        {
            yAtual = y;
        }*/
        float posX = Mathf.SmoothDamp(transform.position.x,player.position.x,ref velocity.x,delayX);
        float posY = Mathf.SmoothDamp(transform.position.y,player.position.y, ref velocity.y,delayY);

        transform.position = new Vector3(posX, posY + yAtual, transform.position.z);

        //Ativa ou desativar a max e a min posisão da camera

        if (bounds)
        { 
            transform.position = new Vector3(Mathf.Clamp(transform.position.x,minCameraPos.x,maxCameraPos.x),Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y), Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }
}
