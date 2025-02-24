using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// public interface IHintEvents : IEventSystemHandler
// {
//     void buttonsClicked();
// }

public class Hint : MonoBehaviour
{
    public Image ToggleButton; // Reference to the Image component
    public Image HintImage;
    public int timer;

    // Start is called before the first frame update
    void Start()
    {
        HintImage.enabled = false;
        ToggleButton.enabled = false;
        StartCoroutine(StartTimer());
    }

    public void ToggleHint()
    {
        HintImage.enabled = !HintImage.enabled;
    }

    // Update is called once per frame

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timer);
        ToggleButton.enabled = true;
    }
}
