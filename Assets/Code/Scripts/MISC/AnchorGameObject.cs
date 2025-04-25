using System.Collections;
using UnityEngine;

// Code modified from Awesometuts,
// https://awesometuts.com/blog/support-mobile-screen-sizes-unity/?utm_medium=video&utm_source=youtube&utm_campaign=how_to_make_your_game_look_the_same_on_all_mobile_screen_sizes

[ExecuteAlways]
public class AnchorGameObject : MonoBehaviour
{
    public enum AnchorType
    {
        BottomLeft,
        BottomCenter,
        BottomRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        TopLeft,
        TopCenter,
        TopRight,
    }

    public AnchorType anchorType;
    public Vector3 anchorOffset;

    void Start()
    {
        StartCoroutine(AnchorWhenReady());
    }

    IEnumerator AnchorWhenReady()
    {
        uint cameraWaitCycles = 0;

        while (CameraViewportHandler.Instance == null)
        {
            ++cameraWaitCycles;
            yield return null;
        }

#if UNITY_EDITOR
        if (cameraWaitCycles > 0)
        {
            Debug.Log(
                $"AnchorGameObject waited {cameraWaitCycles} frame(s) for CameraViewportHandler.Instance. "
                    + "Consider adjusting script execution order if needed."
            );
        }
#endif
        UpdateAnchor();
    }

    void UpdateAnchor()
    {
        if (CameraViewportHandler.Instance == null)
            return;

        switch (anchorType)
        {
            case AnchorType.BottomLeft:
                SetAnchor(CameraViewportHandler.Instance.BottomLeft);
                break;
            case AnchorType.BottomCenter:
                SetAnchor(CameraViewportHandler.Instance.BottomCenter);
                break;
            case AnchorType.BottomRight:
                SetAnchor(CameraViewportHandler.Instance.BottomRight);
                break;
            case AnchorType.MiddleLeft:
                SetAnchor(CameraViewportHandler.Instance.MiddleLeft);
                break;
            case AnchorType.MiddleCenter:
                SetAnchor(CameraViewportHandler.Instance.MiddleCenter);
                break;
            case AnchorType.MiddleRight:
                SetAnchor(CameraViewportHandler.Instance.MiddleRight);
                break;
            case AnchorType.TopLeft:
                SetAnchor(CameraViewportHandler.Instance.TopLeft);
                break;
            case AnchorType.TopCenter:
                SetAnchor(CameraViewportHandler.Instance.TopCenter);
                break;
            case AnchorType.TopRight:
                SetAnchor(CameraViewportHandler.Instance.TopRight);
                break;
        }
    }

    void SetAnchor(Vector3 anchor)
    {
        Vector3 newPos = anchor + anchorOffset;
        if (!transform.position.Equals(newPos))
        {
            transform.position = newPos;
        }
    }
}
