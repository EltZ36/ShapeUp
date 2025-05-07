using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScore : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite winningScoreImage;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        NetSFX.Swish += SetScore;
    }

    private void SetScore()
    {
        sr.sprite = winningScoreImage;
    }

    public void OnDisable()
    {
        NetSFX.Swish -= SetScore;
    }
}
