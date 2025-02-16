using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExistShape : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector2 offset;

    public void InstantiateTarget(EventInfo eventInfo)
    {
        Instantiate(
            target,
            eventInfo.TargetObject.gameObject.transform.position + (Vector3)offset,
            Quaternion.identity
        );
    }

    public void DestroyTarget(EventInfo eventInfo)
    {
        Destroy(target);
    }

    public void TakeApart(EventInfo eventInfo)
    {
        ShapeManager.Instance.TakeApartShape(eventInfo.TargetObject.GetComponent<Shape>());
    }
}
