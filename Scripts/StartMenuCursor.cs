using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void OnGUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
