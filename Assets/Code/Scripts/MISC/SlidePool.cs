using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlidePool : PoolObject
{
    // Start is called before the first frame update
    public GameObject objectToPool2;

    [SerializeField]
    private Transform ResetPositionTransform;

    public override void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        GameObject tmp2;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(true);
            tmp2 = Instantiate(objectToPool2);
            tmp2.SetActive(true);
            pooledObjects.Add(tmp);
        }
    }

    void Awake()
    {
        SharedInstance = this;
    }

    public void ResetPosition(GameObject obj)
    {
        if (obj != null)
        {
            obj.transform.position = ResetPositionTransform.position;
            obj.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (ReferenceEquals(objectToPool, collision.gameObject))
        {
            // If the collided object is the same as the pooled object, remove it
            RemovePooledObject(collision.gameObject);
            ResetPosition(collision.gameObject);
        }
        else if (ReferenceEquals(objectToPool2, collision.gameObject))
        {
            RemovePooledObject(collision.gameObject);
            ResetPosition(collision.gameObject);
        }
    }
}