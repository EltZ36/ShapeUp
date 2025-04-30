using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubLevelWin : MonoBehaviour
{
    [SerializeField]
    protected Shape shapeTarget;

    [SerializeField]
    protected GameObject winEffect1,
        winEffect2;

    protected string targetName;

    protected virtual void Awake()
    {
        targetName = shapeTarget.ShapeName;
    }

    protected void OnWin(GameObject obj)
    {
        Shape shape = obj.GetComponent<Shape>();
        if (shape != null && shape.ShapeName == targetName)
        {
            PlayFireworks(shape.ShapeName);
            StartCoroutine(CameraController.ZoomOut(false));
            Destroy(shape.gameObject);
            Physics2D.gravity = new Vector2(0f, -9.8f);
            LevelManager.Instance.OnCurrentSubLevelComplete();
        }

        Destroy(obj);
    }

    protected void PlayFireworks(string _shape)
    {
        winEffect1.SetActive(true);
        winEffect2.SetActive(true);

        // Animator anim1 = winEffect1.GetComponent<Animator>();
        // Animator anim2 = winEffect2.GetComponent<Animator>();

        // anim1.Play("Base Layer." + _shape + "Win");
        // anim2.Play("Base Layer." + _shape + "Win");
    }
}
