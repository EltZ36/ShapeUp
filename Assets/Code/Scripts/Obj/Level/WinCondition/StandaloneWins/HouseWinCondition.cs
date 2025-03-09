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

    [SerializeField]
    GameObject winEffect1,
        winEffect2;

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
            PlayFireworks(active[0], active[1]);
            Invoke();
            zoomCanvas.gameObject.SetActive(false);
            //StartCoroutine(CameraController.ZoomOut(false));
        }
    }

    void PlayFireworks(string _shape1, string _shape2)
    {
        winEffect1.SetActive(true);
        winEffect2.SetActive(true);

        Animator anim1 = winEffect1.GetComponent<Animator>();
        Animator anim2 = winEffect2.GetComponent<Animator>();

        anim1.Play("Base Layer." + _shape1 + "Win", 5);
        anim2.Play("Base Layer." + _shape2 + "Win", 5);
    }

    public void Invoke()
    {
        ExecuteEvents.Execute<IStandAloneWinEvent>(target, null, (x, y) => x.OnWin());
    }
}
