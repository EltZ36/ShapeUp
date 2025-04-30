using UnityEngine;

public class CollisionWin : SubLevelWin
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        OnWin(collision.gameObject);
    }
}
