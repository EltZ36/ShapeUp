using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateWin : MonoBehaviour
{
    [SerializeField]
    Shape[] shapes;

    string[] names;

    List<string> active = new List<string>();

    [SerializeField]
    GameObject winEffect1,
        winEffect2;

    void Awake()
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
            Debug.Log("Victory");
            PlayFireworks(active[0], active[1]);
            LevelManager.Instance.OnCurrentSubLevelComplete();
            StartCoroutine(CameraController.ZoomOut(false));
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
}
