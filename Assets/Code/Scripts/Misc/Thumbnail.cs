using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thumbnail : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    //Below should be split up and put into the correct locations, this is temp
    void Update()
    {
        if (mainCamera.orthographicSize < 6)
        {
            Vector2 pull = Snap(mainCamera.transform.position, transform.position, 2, 20);
            if (pull.magnitude > 0.1)
            {
                mainCamera.transform.position += (Vector3)pull * Time.deltaTime;
            }
            if (Vector2.Distance(mainCamera.transform.position, transform.position) < 2)
            {
                // TODO: when making the actual levels, we will need a mapping of poistion to sublevel id
                // that way we will know what to put V there
                LevelManager.Instance.InjectSubLevel(0, transform.position);
            }
        }
    }

    private Vector3 Snap(Vector3 position, Vector3 anchor, float snapDist, float strength)
    {
        float distance = Vector2.Distance(position, anchor);
        if (distance < snapDist)
        {
            Vector2 direction = anchor - position;
            return direction.normalized * (distance > strength ? strength : distance);
        }
        return Vector2.zero;
    }
}
