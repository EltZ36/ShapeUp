using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class BoundaryWin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Shape shapeTarget;

    [SerializeField]
    GameObject winEffect1,
        winEffect2;

    string targetName;

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
                PlayFireworks(shape.ShapeName);
                StartCoroutine(CameraController.ZoomOut(false));
                Destroy(shape.gameObject);
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
