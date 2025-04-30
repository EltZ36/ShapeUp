using UnityEngine;

public class PanBouncer : MonoBehaviour
{
    [SerializeField]
    Transform forcePoint;

    [SerializeField]
    float minForce = 1f,
        maxForce = 2f;

    [SerializeField]
    float checkInterval = 0.1f;

    Rigidbody2D panBody;
    float prevVelocityY = 0f;

    void Awake()
    {
        panBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        InvokeRepeating(nameof(CheckForBounce), 0f, checkInterval);
    }

    void CheckForBounce()
    {
        float currentVelocityY = panBody.velocity.y;

        if (prevVelocityY < 0f && currentVelocityY > 0f)
        {
            Vector2 force = new Vector2(0f, Random.Range(minForce, maxForce));
            panBody.AddForceAtPosition(force, forcePoint.position, ForceMode2D.Impulse);
        }

        prevVelocityY = currentVelocityY;
    }
}
