using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Camera cam;
    private bool pinching;
    private float scale;

    float minSize,
        maxSize;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        scale = gameObject.transform.localScale.x;
        minSize = 0.5f;
        maxSize = 3f;
        // pinchPoint.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length == 2)
        {

            handleTwoTouches();
        }
        // pinchPoint.GetComponent<SpriteRenderer>().enabled = !pinching;
    }

    void handleTwoTouches()
    {
        Vector3 pointOne = cam.ScreenToWorldPoint(Input.touches[0].position);
        Vector3 pointTwo = cam.ScreenToWorldPoint(Input.touches[1].position);
        Vector2 pointOne2D = new Vector2(pointOne.x, pointOne.y);
        Vector2 pointTwo2D = new Vector2(pointTwo.x, pointTwo.y);
        if (!pinching)
        {
            Vector2 midPoint = new Vector2(
                (pointOne2D.x + pointTwo2D.x) / 2,
                (pointOne2D.y + pointTwo2D.y) / 2
            );
            // GameObject.FindGameObjectWithTag("midpoint").transform.position = midPoint;
            RaycastHit2D hitInformation = Physics2D.Raycast(midPoint, cam.transform.forward);
            if (hitInformation.collider != null)
            {
                if (hitInformation.transform.gameObject == gameObject)
                {
                    scale = Vector2.Distance(pointOne2D, pointTwo2D);
                    pinching = true;
                }
            }
        }
        else
        {
            Vector3 scaleChange = new Vector3(
                Mathf.Clamp(
                    gameObject.transform.localScale.x
                        + (Vector2.Distance(pointTwo2D, pointOne2D) - scale) / 2f,
                    minSize,
                    maxSize
                ),
                Mathf.Clamp(
                    gameObject.transform.localScale.y
                        + (Vector2.Distance(pointTwo2D, pointOne2D) - scale) / 2f,
                    minSize,
                    maxSize
                ),
                1f
            );
            gameObject.transform.localScale = scaleChange;
            scale = Vector2.Distance(pointTwo2D, pointOne2D);
        }
        if (
            Input.touches[0].phase == TouchPhase.Ended
            || Input.touches[1].phase == TouchPhase.Ended
        )
        {
            pinching = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("win"))
        {
            Debug.Log("Victory");
        }
    }
}
