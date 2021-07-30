using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData 
{
    // Start is called before the first frame update
    public float[] playerPosition;

    public bool GateOpen;
    public bool MonsterOn;
    public bool object1On;
    public bool object2On;
    public bool object3On;


    public PlayerData(DetectWithMouse player)
    {
        playerPosition = new float[3];
        playerPosition[0] = player.fpsCam.GetComponentInParent<Transform>().position.x;
        playerPosition[1] = player.fpsCam.GetComponentInParent<Transform>().position.y;
        playerPosition[2] = player.fpsCam.GetComponentInParent<Transform>().position.z;
 
        GateOpen = player.objectGate.activeSelf;
        MonsterOn = player.monster.activeSelf;
        object1On = player.object1.activeSelf;
        object2On = player.object2.activeSelf;
        object3On = player.object3.activeSelf;

    }

}
