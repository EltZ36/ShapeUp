using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnteredTrigger : MonoBehaviour
{
    [SerializeField]
    private string newLayer = "InsideBox";

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody != null)
        {
            other.gameObject.layer = LayerMask.NameToLayer(newLayer);
        }
    }
}
