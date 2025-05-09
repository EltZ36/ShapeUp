using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightTapWin : MonoBehaviour
{
    [SerializeField]
    Shape shapeTarget;

    [SerializeField]
    GameObject lights;

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
                lights.GetComponent<Light2D>().intensity = 1;
                PlayFireworks(shape.ShapeName);
                StartCoroutine(CameraController.ZoomOut(false));
                Destroy(shape.gameObject);
                Physics2D.gravity = new Vector2(0f, -9.8f);
                LevelManager.Instance.OnCurrentSubLevelComplete();
            }
        }
        Destroy(collision.gameObject);
    }

    void PlayFireworks(string _shape)
    {
        winEffect1.SetActive(true);
        winEffect2.SetActive(true);
    }
}
