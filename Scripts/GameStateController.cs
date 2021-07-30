using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;

public  class GameStateController : MonoBehaviour
{

    //some variables and behaviours were moved from the Player to this class to make
    //the game more organised
    
    //duration of the level in seconds
  //  public static float levelDuration;

    //controls whether the game is running (true) or paused/over (false)
    public static bool gameRunning;

    //points that the player has
    //  public static int score;
    public GameObject Player;
    //lives that the player has
    // public static int lives;

    public MouseLook mouseLook;
    public GameObject ghostOne;
    public GameObject ghostTwo;
    public GameObject ghostThree;
    public Text dialogueText;
    //reference to the DialogueSystem (another component in the same GameObject)
    private DialogueSystem dialogueSys;
    //reference to the UIController (another component in the same GameObject)
    private UIController ui;

    //true if the game is stopped at an event (e.g. cutscene), false if not
    private static bool isInEvent;

    //holds the current (or the last event) that happened in game
    private DialogueLine currentEvent;

    //reference to allObstacles - this is used to make the player temporarily invincible after
    //hitting an obstacle, so the game does not stop/break
 //   public GameObject allObstacles;

    // Start is called before the first frame update

    [System.Obsolete]
    void Start()
    {
        /*  score = 0;

          lives = 3;

          levelDuration = 99999;*/
       // mouseLook.Init(Player.transform, Player.transform.FindChild("FirstPersonCharacter").transform);

        dialogueSys = GetComponent<DialogueSystem>();
        ui = GetComponent<UIController>();

        isInEvent = false;

        gameRunning = true;

        //string path = Application.dataPath + "/StreamingAssets/save/player.cxy";
        //if (!File.Exists(path))
        //{
            StartEvent(48);
       // }
        //updates the ui using the variable lives above (Check UIController if needed)
      //  ui.RefreshLives();

        //updates the score using the variable score above (Check UIController if needed)
   //     ui.RefreshScore();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (isInEvent)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    MoveToNextEvent();
                }
            }
        
    }

    public void Pause()
    {
        gameRunning = false;
        Time.timeScale = 0f;

    }

    //restores gameplay
    public void Unpause()
    {
        gameRunning = true;
        Time.timeScale = 1f;

    }

    //This is usually called by a TriggerEvent
    //Finds the right event and displays the static cutscene
    public void StartEvent(int eventID)
    {
        //pauses gameplay
        Pause();

        //this bool is used to control whether update should read and use clicks (see Update function above)
        isInEvent = true;

        //Retrieves the correct "narrative event" (as a DialogueLine) using the eventid received from whoever called this function
        //(usually the trigger)
        currentEvent = dialogueSys.GetEvent(eventID);      

        //passes the DialogueLine object to the ui so it can display the content (text, image, audio)
        ui.DisplayEvent(currentEvent);        

    }

    //THis is usually called from Update
    //when there is a click and "isInEvent" is true
    public void MoveToNextEvent()
    {        
        //if this is the last event in the sequence we need to get back to gameplay
        if (currentEvent.lastEventInSequence)
        {
            //the game will be unpaused
            Unpause();
            //the dialogue box will be hidden
            ui.HideDialogue();
            //and the bool isInEvent will be set to false (as we are back to gameplay)
            isInEvent = false;
        }
        //otherwise, we start the next "narrative event" in that sequence
        else
        {
            //see function above
            StartEvent(currentEvent.nextEvent);
        }

        if (currentEvent.ghostControl == 1)
        {
            ghostOne.SetActive(false);
        }
        if (currentEvent.ghostControl == 2)
        {
            ghostTwo.SetActive(false);
        }
        if (currentEvent.ghostControl == 3)
        {
            ghostThree.SetActive(false);
        }


    }

   


 /*   public void UpdateScore(int points)
    {
        score += points;
        ui.RefreshScore();
    }


    public void UpdateLives(int difference)
    {
        lives += difference;
        ui.RefreshLives();
    }


    public void MakeInvincible()
    {
        foreach (Collider c in allObstacles.GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
    }

    public void MakeVulnerable()
    {
        foreach (Collider c in allObstacles.GetComponentsInChildren<Collider>())
        {
            c.enabled = true;
        }
    }*/
}
