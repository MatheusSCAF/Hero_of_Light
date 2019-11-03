using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField] private PowerUpsController powerUps;
    private GameObject player;
    private PlayerBehaviour pBehaviour;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        pBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }
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

