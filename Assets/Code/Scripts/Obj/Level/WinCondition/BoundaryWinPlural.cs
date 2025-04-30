using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class BoundaryWinPlural : MonoBehaviour
{
    [SerializeField]
    private List<Shape> shapeTargets = new List<Shape>();

    [SerializeField]
    GameObject winEffect1,
        winEffect2;

    private List<string> targetName = new List<string>();
    private int counter;

    void Awake()
    {
        foreach (var shape in shapeTargets)
        {
            targetName.Add(shape.ShapeName);
        }
        counter = shapeTargets.Count;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Shape shape = collision.gameObject.GetComponent<Shape>();
        if (shape != null)
        {
            if (targetName.Contains(shape.ShapeName))
            {
                Destroy(shape.gameObject);
                counter--;
            }
            if (counter <= 0)
            {
                PlayFireworks(shape.ShapeName);
                StartCoroutine(CameraController.ZoomOut(false));
                Physics2D.gravity = new UnityEngine.Vector2(0f, -9.8f);
                LevelManager.Instance.OnCurrentSubLevelComplete();
            }
        }
        Destroy(collision.gameObject);
    }

    void PlayFireworks(string _shape)
    {
        winEffect1.SetActive(true);
        winEffect2.SetActive(true);

        // Animator anim1 = winEffect1.GetComponent<Animator>();
        // Animator anim2 = winEffect2.GetComponent<Animator>();

        // anim1.Play("Base Layer." + _shape + "Win");
        // anim2.Play("Base Layer." + _shape + "Win");
    }
}
