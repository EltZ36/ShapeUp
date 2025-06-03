using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltPool : PoolObject
{
    public static SaltPool SaltInstance;

    [SerializeField]
    private int amountToRemove = 5;
    private int amountRemoved = 0;
    public Transform poolPosition;

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
                Random.Range(poolPosition.position.x - 0.4f, poolPosition.position.x + .4f),
                Random.Range(poolPosition.position.y - 0.4f, poolPosition.position.y + .4f),
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
