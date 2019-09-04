using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public PowerUpsController powerUps;
    public void Destroir()
    {
        Destroy(this.gameObject);
    }
}

