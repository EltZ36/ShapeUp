using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StandAloneWin;
using UnityEngine;
using UnityEngine.EventSystems;

public class HouseWinCondition : MonoBehaviour, IStandAloneWin
{
    public GameObject target;

    [SerializeField]
    Shape[] shapes;

    string[] names;

    public Canvas zoomCanvas;
    List<string> active = new List<string>();

    void Start()
    {
        names = shapes.Select(s => s.ShapeName).ToArray();
        ShapeManager.Instance.OnDestroyShape += RemoveShape;
        ShapeManager.Instance.OnCreateShape += AddShape;
    }

    void OnDestroy()
    {
        ShapeManager.Instance.OnDestroyShape -= RemoveShape;
        ShapeManager.Instance.OnCreateShape -= AddShape;
    }

    void AddShape(Shape shape)
    {
        active.Add(shape.ShapeName);
        CheckActive();
    }

    void RemoveShape(Shape shape)
    {
        active.Remove(shape.ShapeName);
    }

    void CheckActive()
    {
        if (ShapeManager.ContainsSet(active.ToArray(), names))
        {
            Invoke();
            zoomCanvas.gameObject.SetActive(false);
            //StartCoroutine(CameraController.ZoomOut(false));
        }
    }

    public void Invoke()
    {
        ExecuteEvents.Execute<IStandAloneWinEvent>(target, null, (x, y) => x.OnWin());
    }
}
