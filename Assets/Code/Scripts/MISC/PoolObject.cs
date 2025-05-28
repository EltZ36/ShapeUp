using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    // Start is called before the first frame update

    public static PoolObject SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    public virtual void Start()
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

    public virtual void RemovePooledObject(GameObject obj)
    {
        if (obj != null && pooledObjects.Contains(obj))
        {
            obj.SetActive(false);
        }
    }

    public virtual void SpawnPoolObject()
    {
        GameObject obj = SharedInstance.GetPooledObject();
        if (obj != null)
        {
            obj.SetActive(true);
        }
    }
}
