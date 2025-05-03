using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterWin : MonoBehaviour
{
    [SerializeField]
    private BoundaryChecker cubePlate,
        conePlate;

    [SerializeField]
    GameObject winEffect1,
        winEffect2;

    public void checkWin()
    {
        if (cubePlate.shapeInside && conePlate.shapeInside)
        {
            //PlayFireworks(shape.ShapeName);
            Debug.Log("Win!");
            LevelManager.Instance.OnCurrentSubLevelComplete();
            StartCoroutine(CameraController.ZoomOut(false));
        }
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
