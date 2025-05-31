using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlidePool : PoolObject
{
    // Start is called before the first frame update
    public GameObject objectToPool2;

    [SerializeField]
    private Transform ResetPositionTransform,
        ResetPositionTransform2;

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
            pooledObjects.Add(tmp2);
        }
    }

    void Awake()
    {
        SharedInstance = this;
    }

    public void ResetPosition(GameObject obj)
    {
        if (obj != null && pooledObjects.Contains(obj))
        {
            if (obj.tag == "Cubehead")
            {
                obj.transform.position = ResetPositionTransform.position;
            }
            if (obj.tag == "Conehead")
            {
                obj.transform.position = ResetPositionTransform2.position;
            }
            obj.transform.rotation = Quaternion.Euler(0, 0, 0);
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Attempted to reset position of a null object.");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.gameObject.CompareTag("Cubehead") == true
            || collision.gameObject.CompareTag("Conehead") == true
        )
        {
            // If the collided object is the same as the pooled object, remove it
            RemovePooledObject(collision.gameObject);
            ResetPosition(collision.gameObject);
        }
    }
}
