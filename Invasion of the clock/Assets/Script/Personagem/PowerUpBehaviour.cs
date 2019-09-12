using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PowerUpBehaviour : MonoBehaviour
{
    public PowerUpsController powerUps;
    public void Destroir()
    {
        Destroy(this.gameObject);
    }
}

