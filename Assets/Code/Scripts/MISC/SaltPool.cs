using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltPool : PoolObject
{
    public static SaltPool SaltInstance;

    [SerializeField]
    private int amountToRemove = 5;
    private int amountRemoved = 0;

    void Awake()
    {
        SaltInstance = this;
    }

    public override void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(true);
            tmp.transform.position = new Vector3(
                Random.Range(0.9f, 1f),
                Random.Range(1.1f, 1.363f),
                0
            );
            pooledObjects.Add(tmp);
        }
    }

    public void RemoveMultipleObjects()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (obj.activeInHierarchy)
            {
                obj.SetActive(false);
                amountRemoved++;
                if (amountRemoved == amountToRemove)
                {
                    amountRemoved = 0;
                    break;
                }
            }
        }
    }
}
