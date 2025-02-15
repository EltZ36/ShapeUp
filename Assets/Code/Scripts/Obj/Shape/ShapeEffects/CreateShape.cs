using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShape : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    Vector2 offset;

    public void InstantiatePrefab(EventInfo eventInfo)
    {
        Instantiate(
            prefab,
            eventInfo.TargetObject.gameObject.transform.position + (Vector3)offset,
            Quaternion.identity
        );
    }
}
