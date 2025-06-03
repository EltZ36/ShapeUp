using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSliders : MonoBehaviour
{
    [SerializeField]
    private Transform slidePosition;

    [SerializeField]
    private Vector3 startingOffset;

    void Awake(){
        gameObject.transform.position = slidePosition.position + startingOffset;
    }
}
