using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSliders : MonoBehaviour
{
    [SerializeField]
    private Vector3 slidePosition;

    void Awake(){
        gameObject.transform.position = slidePosition + gameObject.transform.position;
    }
}
