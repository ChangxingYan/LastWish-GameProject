using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvokeButton : MonoBehaviour
{
    public Button button;

    void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();
    }

    public void Invoke()
    {
        if (button != null && button.onClick != null)
            button.onClick.Invoke();
    }
}
