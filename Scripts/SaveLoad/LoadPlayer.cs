using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityStandardAssets.Characters.FirstPerson;
public class LoadPlayer : MonoBehaviour
{


    public bool GateOpen;
    public bool MonsterOn;
    public bool object1On;
    public bool object2On;
    public bool object3On;
    public DetectWithMouse PlayerController;
    public CanvasGroupAlpha Canvas;
    public void Start()
    {

        Canvas.FadeIn();
        PlayerController = FindObjectOfType<DetectWithMouse>();
    }

    void OnLevelWasLoaded()
    {

        Canvas.FadeIn();
        PlayerController = FindObjectOfType<DetectWithMouse>();
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(PlayerController);
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position =Vector3.zero;
        position.x = data.playerPosition[0];
        position.y = data.playerPosition[1];
        position.z = data.playerPosition[2]; 
        PlayerController.fpsCam.GetComponentInParent<CharacterController>().enabled = false;
        PlayerController.fpsCam.GetComponentInParent<FirstPersonController>().enabled= false;
        PlayerController.fpsCam.GetComponentInParent<Transform>().parent.position = position;

        PlayerController.fpsCam.GetComponentInParent<FirstPersonController>().enabled = true;
        PlayerController.fpsCam.GetComponentInParent<CharacterController>().enabled = true;

        GateOpen = data.GateOpen;
        MonsterOn = data.MonsterOn;
        object1On = data.object1On;
        object2On = data.object2On;
        object3On = data.object3On;
        PlayerController.objectGate.SetActive(GateOpen);
        PlayerController.object1.SetActive(object1On);
        PlayerController.object2.SetActive(object2On);
        PlayerController.object3.SetActive(object3On);
        PlayerController.monster.SetActive(MonsterOn);
    }
}
