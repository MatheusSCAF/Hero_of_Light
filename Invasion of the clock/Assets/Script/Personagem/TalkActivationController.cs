using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkActivationController : MonoBehaviour
{
    public GameObject seta;

    public void setaAparecer()
    {
        seta.SetActive(true);
    }
    public void setaDesaparecer()
    {
        seta.SetActive(false);
    }
 }
