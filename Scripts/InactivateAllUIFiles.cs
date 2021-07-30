using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactivateAllUIFiles : MonoBehaviour
{

    Transform[] allChildren;
    public void Inactivate()
    {
        allChildren = this.GetComponentsInChildren<Transform>();

        foreach(Transform t in allChildren)
        {
            t.gameObject.SetActive(false);
        }

    }

}
