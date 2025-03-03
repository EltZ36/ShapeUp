using StandAloneWin;
using UnityEngine;
using UnityEngine.EventSystems;

public class PiggyBank : MonoBehaviour, IStandAloneWin
{
    // specific behaviours
    public Shape shapeTarget;
    public GameObject winEffect1,
        winEffect2;
    string targetName;

    // IMPORTANT needed to reference game object that implements StandaloneWin
    public GameObject target;

    void Awake()
    {
        targetName = shapeTarget.ShapeName;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Shape shape = collision.gameObject.GetComponent<Shape>();
        if (shape != null)
        {
            if (shape.ShapeName == targetName)
            {
                Destroy(shape.gameObject);
                Invoke();
            }
        }
        Destroy(collision.gameObject);
    }

    // IMPORTANT -> use this to send message to the standalone win module.
    public void Invoke()
    {
        ExecuteEvents.Execute<IStandAloneWinEvent>(target, null, (x, y) => x.OnWin());
    }
}
