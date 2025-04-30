using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField]
    GameObject targetObject;

    public void activatePrefab()
    {
        Vector3 targetPos = new Vector3(-0.52f, 0f, 0f);
        Instantiate(targetObject, targetPos, Quaternion.identity).SetActive(true);
    }
}
