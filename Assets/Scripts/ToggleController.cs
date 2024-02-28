using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleController : MonoBehaviour
{
    public GameObject object1;
    public TMP_Text textToShowHide;

    void Start()
    {
        if (object1 != null)
        {
            object1.SetActive(false);
        }

        if (textToShowHide != null)
        {
            textToShowHide.text = "Show";
        }
    }

    public void Toggle()
    {
        if (object1 != null)
        {
            bool newState = !object1.activeSelf;
            object1.SetActive(newState);

            if (textToShowHide != null)
            {
                textToShowHide.text = newState ? "Hide" : "Show";
            }
        }
    }
}
