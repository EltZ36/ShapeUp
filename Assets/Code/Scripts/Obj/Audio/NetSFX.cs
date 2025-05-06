using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetSFX : MonoBehaviour
{
    public AudioClip swishSound;
    public Collider2D basketball;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col == basketball)
        {
            AudioManager.Instance.Play(false, swishSound, 0);
        }
    }
}
