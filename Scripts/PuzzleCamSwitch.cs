using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PuzzleCamSwitch : MonoBehaviour
{
    bool camSwitch = false;
    public Camera fpsCam;
    public Camera puzzleCam;
    public GameStateController gameCon;
    AudioListener audio;
    private FirstPersonController fpsController;



    // Start is called before the first frame update
   void Start()
    {
        fpsController = fpsCam.gameObject.GetComponent<FirstPersonController>() ;


        fpsCam.enabled = true;
        puzzleCam.enabled = false;
        audio = puzzleCam.transform.GetComponent<AudioListener>();

    }

    // Update is called once per frame
    private void Update()
    {
     //  if (camSwitch && test)
     //  {
     //
     //      Cursor.lockState = CursorLockMode.None;
     //      Cursor.visible = true;
     //      Debug.Log(Cursor.visible);
     //      test = false;
     //  }
    }
    public void GoToPuzzle()
    {

        if(fpsCam != null)
        {

            fpsCam.transform.parent.gameObject.SetActive(camSwitch);
            fpsCam.enabled = !fpsCam.enabled;
            puzzleCam.enabled = !puzzleCam.enabled;
            audio.enabled = !audio.enabled;
            camSwitch = !camSwitch;

            if (camSwitch)
            {
                gameCon.gameObject.GetComponent<DetectWithMouse>().enabled = false;
             Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            }
            else
            {
                gameCon.gameObject.GetComponent<DetectWithMouse>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

         
        }


    }


}
