using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLine : ScriptableObject // this is a ScriptableObject because we are using this class
    //as a "structure" only - we do not need it as a behaviour within a particular GameObject
{

    public int id;                      //the id of the event in the file
    public string speaker;              //who (the character) is speaking in this event
    public string text;                 //the text that will be displayed on the screen
    public int nextEvent;               //the id of the following event
    public int ghostControl;         //controls the disappearance of ghost 1 2 3
    public bool lastEventInSequence;    //true if this is the last event in a particular sequence, false is there is a following event (speech)
    public string imgAsset;             //the name of the image asset associated with that event
    public string audioAsset;           //the name of the audio asset associated with that event
    

}
