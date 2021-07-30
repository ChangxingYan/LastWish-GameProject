using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UITriggerEvent : MonoBehaviour
{
    public GameObject panel;

    public delegate void OnTurnOnPanel();
    private OnTurnOnPanel onTurnOnPanel;

    public delegate void OnTurnOffPanel();
    public OnTurnOffPanel onTurnOffPanel;

    Transform[] allChildren;
    private FirstPersonController firstPersonController;

    public void Start()
    {
        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        onTurnOnPanel += firstPersonController.CursorLock; 
        onTurnOffPanel += firstPersonController.CursorUnlock;
            


    }

    public void TurnOnPanel()
    {
       if(onTurnOnPanel!= null)
        {
            onTurnOnPanel();
        }

           
        if (panel)
        {

            allChildren = panel.GetComponentsInChildren<Transform>(true);

            foreach (Transform t in allChildren)
            {
                           t.gameObject.SetActive(true);

            }
            panel.transform.parent.gameObject.SetActive(true);


        }
        

    }
    public void TurnOffPanel()
    {
        if (onTurnOffPanel != null)
        {
            onTurnOffPanel();
        }

    }


}
