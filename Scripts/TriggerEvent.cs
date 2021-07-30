using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public int initialEventID;
    public int collectiveEventID;

    private GameStateController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameStateController>();
    }

    public void CallStory()
    {
        gameController.StartEvent(this.initialEventID);

    }

    public void CollectStory()
    {
        gameController.StartEvent(this.collectiveEventID);
    }
}
