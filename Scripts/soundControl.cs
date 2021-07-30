using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundControl : MonoBehaviour
{
    public AudioSource audioControl;


    void GetThingsSound()
    {
        if (gameObject.activeSelf)
        {
            audioControl.Play();
        }
    }
}
