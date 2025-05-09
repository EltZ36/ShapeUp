using System.Collections.Generic;
using UnityEngine;

public class LightTap : MonoBehaviour
{
    [SerializeField]
    private GameObject lightHolder;
    private int lightCounter;
    private const int maxLights = 5;
    private List<GameObject> lightPool;

    void Start()
    {
        lightCounter = 0;
        lightPool = new List<GameObject>();
        Transform[] lights = lightHolder.GetComponentsInChildren<Transform>();
        foreach (Transform templight in lights)
        {
            if (templight != lights[0])
            {
                lightPool.Add(templight.gameObject);
                templight.SetLocalPositionAndRotation(
                    new Vector3(1000f, 1000f, 1000f),
                    templight.localRotation
                );
            }
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
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

        if (hit.collider == null && lightCounter < maxLights)
        {
            lightCounter++;
            GameObject newLight = GetLight();
            if (newLight != null)
            {
                newLight.GetComponent<EffectTapGrow>().ResetShape();
                newLight.transform.position = tapPos;
            }
        }
    }

    private GameObject GetLight()
    {
        for (int i = 0; i < lightPool.Count; i++)
        {
            if (lightPool[i].transform.position.x == 1000f)
            {
                return lightPool[i];
            }
        }
        return null;
    }

    public void DecrementLights()
    {
        lightCounter -= 1;
    }
}
