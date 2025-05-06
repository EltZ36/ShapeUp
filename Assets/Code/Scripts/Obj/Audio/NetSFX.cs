using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NetSFX : MonoBehaviour
{
    public AudioClip swishSound;
    public Collider2D basketball;
    public static event Action Swish;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col == basketball)
        {
            AudioManager.Instance.Play(false, swishSound, 0);
            Swish.Invoke();
        }
    }
}
