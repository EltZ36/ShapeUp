using System.Collections.Generic;
using UnityEngine;

public class LightTap : MonoBehaviour
{
    //NOTE: SCRIPT DOES NOT WORK
    [SerializeField]
    GameObject lightHolder;
    private int lightCounter = 0;
    private const int maxLights = 5;
    private List<GameObject> lightPool;

    void Start()
    {
        lightCounter = 0;
        lightPool = new List<GameObject>();
        Transform[] lights = lightHolder.GetComponentsInChildren<Transform>();
        foreach (Transform t in lights)
        {
            lightPool.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        Debug.Log(lightPool);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("Touch");
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = touch.position;
                HandleTouch(touchPosition);
            }
        }
    }

    private void HandleTouch(Vector2 position)
    {
        Vector2 tapPos = Camera.main.ScreenToWorldPoint(position);
        RaycastHit2D hit = Physics2D.Raycast(tapPos, Camera.main.transform.forward);

        if (hit.collider != null)
        {
            Debug.Log("Hit something.");
            return;
        }
        else if (lightCounter < maxLights)
        {
            Debug.Log("Moving Light");
            lightCounter++;
            GameObject newLight = GetLight();
            if (newLight != null)
            {
                Debug.Log("Found Light");
                newLight.transform.position = position;
                newLight.SetActive(true);
            }
        }
    }

    private GameObject GetLight()
    {
        for (int i = 0; i < lightPool.Count; i++)
        {
            if (!lightPool[i].activeInHierarchy)
            {
                Debug.Log("Yield Light");
                return lightPool[i];
            }
        }
        return null;
    }
}
