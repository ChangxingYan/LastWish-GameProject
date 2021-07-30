using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    
    

    //DialogueBox in the UI
    public UIDialogue dialogueBox;

    //GameOver Message in the UI
   // public GameObject gameOverMessage;

    //AudioSource used to play cutscene sounds
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        //saves the reference to the audiosource
        sound = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //displays an "narrative event" using the DialogueLine object that we retrieved from the DialogueSystem
    //
    public void DisplayEvent(DialogueLine d)
    {
        //shows the UI Dialogue Space
        dialogueBox.gameObject.SetActive(true);
        
        //updates the text according to the DialogueLine
        dialogueBox.currentText.text = d.text;

        //loads the imageAsset and displays it
   //     dialogueBox.speakerImg.sprite = Resources.Load<Sprite>("Art/DialogueImages/" + d.imgAsset);

        //loads the audioClip
        AudioClip c = Resources.Load<AudioClip>("Audio/" + d.audioAsset);

        //if there is an actual audioClip, it is played using the audiosource found at this gameObject.
        if (c)
        {
            sound.PlayOneShot(c);
        }
        
        
        ////OBS: Resources.Load is ok for this level of project (it is still a basic prototype/proof of concept)
        ////In a "proper" build (e.g. a finalised product) you will probably be better off with another loading system
        ////probably an async file loader in the beginning of the level.


    }


    //hides the UI dialogue object
    public void HideDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
    }
}
