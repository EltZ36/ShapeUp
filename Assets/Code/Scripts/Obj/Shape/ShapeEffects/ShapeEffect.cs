using UnityEngine;

[CreateAssetMenu(fileName = "ShapeEffect", menuName = "Shapes/ShapeEffectScriptableObject")]
public class ShapeEffect : ScriptableObject
{
    public void DebugTest(EventInfo eventInfo)
    {
        Debug.Log(eventInfo.TargetObject.name);
    }

    public void DestroyShape(EventInfo eventInfo)
    {
        Destroy(eventInfo.TargetObject);
    }

    public void Move(EventInfo eventInfo)
    {
        eventInfo.TargetObject.transform.position = (Vector2)eventInfo.VectorTwo;
    }

    public void ChangeSizeDist(EventInfo eventInfo)
    {
        eventInfo.TargetObject.transform.localScale =
            Vector3.one * (1 + Vector2.Distance(eventInfo.VectorOne, eventInfo.VectorTwo));
    }
}
