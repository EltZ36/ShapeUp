using System.Collections;
using UnityEngine;

public class EffectHorizontalSlice : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    private Vector3 StartPos;
    private Vector3 CurrentPos;
    private EffectExistShape effectExistShape;
    bool isStarted = true;

    public void setStartPos(EventInfo eventInfo)
    {
        if (isStarted == false)
        {
            return;
        }
        isStarted = true;
        StartPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        setStarted(eventInfo);
        Debug.Log("StartPos: " + StartPos);
    }

    public void checkHorizontalSlice(EventInfo eventInfo)
    {
        if (isStarted == false)
        {
            CurrentPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            var yDiff = Mathf.Abs(CurrentPos.y - StartPos.y);
            var xDiff = Mathf.Abs(CurrentPos.x - StartPos.x);
            //give some leeway for the xdiff but it shouldn't be horizonal
            if (yDiff > 1.5f && xDiff < 0.5f)
            {
                TakeApart(eventInfo);
                DestroyTarget(eventInfo);
            }
        }
        setStarted(eventInfo);
    }

    public void setStarted(EventInfo eventInfo)
    {
        isStarted = !isStarted;
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
