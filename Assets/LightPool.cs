using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.Universal;

public class LightPool : MonoBehaviour
{
    public static LightPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    void Update()
    {
        if (Input.touchCount >= 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Debug.Log("touch");
                SpawnLight();
            }
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void SpawnLight()
    {
        GameObject light = SharedInstance.GetPooledObject();
        if (light != null)
        {
            Debug.Log(Input.GetTouch(0).position);
            light.transform.position =
                Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)
                + new Vector3(0f, 0f, 10f);
            light.SetActive(true);
        }
    }
}
