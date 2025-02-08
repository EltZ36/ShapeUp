using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeAndBreak : MonoBehaviour
{
    Vector3 touchWorldPos,
        startPos,
        endPos;
    public Camera cam;
    public GameObject sliceObject;
    public GameObject ObjectToFloat;
    public GameObject BrokenObject;
    public ParticleSystem Bubbles;

    void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        BrokenObject.SetActive(false);
    }

    private void Swipe()
    {
        //code also based on https://www.youtube.com/watch?v=Pca9LMd8WsM from 00:00 to 1:15
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endPos = cam.ScreenToWorldPoint(
                new Vector3(
                    Input.GetTouch(0).position.x,
                    Input.GetTouch(0).position.y,
                    cam.nearClipPlane
                )
            );
            if (endPos.x > startPos.x || endPos.x < startPos.x)
            {
                Break();
            }
        }
    }

    private void Break()
    {
        RaycastHit2D hit = Physics2D.Linecast(startPos, endPos);
        var ObjectToFloatrb = ObjectToFloat.GetComponent<Rigidbody2D>();
        //Debug.Log(hit.transform);
        if (hit.collider == null)
        {
            Debug.LogWarning("No object hit by the Linecast!");
            return; // Exit the function to prevent null reference errors
        }
        if (hit.transform.gameObject == sliceObject)
        {
            sliceObject = hit.transform.gameObject;
            StartCoroutine(ChangeSprite());
            if (ObjectToFloatrb != null)
            {
                ObjectToFloat.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                Bubbles.Stop();
                Debug.Log("Level Complete");
            }
        }
        else
        {
            Debug.LogWarning("The object hit by the Linecast is not the sliceObject!");
        }
    }

    IEnumerator ChangeSprite()
    {
        yield return new WaitForSeconds(0.1f);
        sliceObject.GetComponent<SpriteRenderer>().enabled = false;
        BrokenObject.SetActive(true);
    }

    void Update()
    {
        Swipe();
    }
}
