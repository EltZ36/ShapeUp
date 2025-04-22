using UnityEngine;

public class BoundaryWin : SubLevelWin
{
    void OnTriggerExit2D(Collider2D collision)
    {
        OnWin(collision.gameObject);
    }
}
