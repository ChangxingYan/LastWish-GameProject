using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PasswordCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private string password = "DOOM";
    public InputField userInput;
    public string userInputString;
    public InteractiveObject interactive;
    public GameObject folkTale;
    public GameObject fieldNotes;
    public AudioSource audioControl;
    AudioSource audio;


    private void Start()
    {
        audio = this.GetComponent<AudioSource>();

    }


    public void CheckPassword()
    {
        userInputString = userInput.text;
        if(userInputString != null)
        {
            if(userInputString == password)
            {

                //interactive.RotationAction();
                folkTale.SetActive(true);
                fieldNotes.SetActive(true);
                audioControl.Play();
            }
            else
            {
                audio.Play();

            }
        }
    }
}
