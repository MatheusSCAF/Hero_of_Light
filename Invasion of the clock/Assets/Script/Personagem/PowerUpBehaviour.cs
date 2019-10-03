using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public PowerUpsController powerUps;
    public GameObject player;
    public PlayerBehaviour pBehaviour;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            switch (powerUps)
            {
                case PowerUpsController.PuloDuplo:
                    pBehaviour.PowerUp(true, false, false);
                    break;
                case PowerUpsController.WallJump:
                    pBehaviour.PowerUp(false, true, false);
                    break;
                case PowerUpsController.CanomBlaster:
                    pBehaviour.PowerUp(false, false, true);
                    break;
                case PowerUpsController.Default:
                    break;
            }
            Destroy(this.gameObject);
        }  
    }
}

