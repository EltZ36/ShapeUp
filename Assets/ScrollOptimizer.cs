using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollOptimizer : MonoBehaviour
{
    void Start() {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        Destroy(GetComponent<HorizontalLayoutGroup>());
        Destroy(GetComponent<ContentSizeFitter>());
    }
}
