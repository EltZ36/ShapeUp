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

    [SerializeField]
    Shape shape;

    public void checkWin()
    {
        if (cubePlate.shapeInside && conePlate.shapeInside)
        {
            if (DailyManager.Instance == null)
            {
                LevelRecorder tracker = FindObjectOfType<LevelRecorder>();
                if (tracker != null)
                {
                    tracker.RecordLevelCompleted();
                }
                else
                {
                    Debug.Log("Level recorder not found");
                }
                PlayFireworks(shape.ShapeName);
                LevelManager.Instance.OnCurrentSubLevelComplete();
                StartCoroutine(CameraController.ZoomOut(false));
            }
            else
            {
                DailyManager.Instance.LoadNextLevel();
            }
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
