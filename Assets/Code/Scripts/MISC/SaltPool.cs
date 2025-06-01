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
        StartCoroutine(loadPool());
    }

    private IEnumerator loadPool()
    {
        yield return null;
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(true);
            tmp.transform.position = new Vector3(
                Random.Range(0.4f, .7f),
                Random.Range(15.1f, 15.363f),
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
