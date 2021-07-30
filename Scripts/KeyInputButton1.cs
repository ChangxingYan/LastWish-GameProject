using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class KeyInputButton1 : MonoBehaviour
{
    // Start is called before the first frame update
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }
    void Update()
    {
        //Check if the Enter key has been pressed.
        if (Input.GetKeyDown(KeyCode.X))
        {

            //Invoke the button's onClick event.
            button.onClick.Invoke();
        }
    }
}
