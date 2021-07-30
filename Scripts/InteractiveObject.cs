using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    [SerializeField] private Vector3 openPosition, closedPosition;
    [SerializeField] private Vector3 toRotation;
    [SerializeField] private float animationTime;
private bool isOpen = false;
    UITriggerEvent UITriggerEvent;
    TriggerEvent TriggerEvent;

    private Hashtable iTweenArgsRot;

    private Hashtable iTweenArgs;
    // Start is called before the first frame update
    void Start()
    {
        iTweenArgs = iTween.Hash();
        iTweenArgs.Add("position", openPosition);
        iTweenArgs.Add("time", animationTime);
        iTweenArgs.Add("islocal", true);
        iTweenArgs.Add("ignoretimescale", true);


        iTweenArgsRot = iTween.Hash();
        iTweenArgsRot.Add("Rotation", toRotation);
        iTweenArgsRot.Add("time", animationTime);
        iTweenArgsRot.Add("ignoretimescale", true);

        UITriggerEvent = this.GetComponent<UITriggerEvent>();
        TriggerEvent = this.GetComponent<TriggerEvent>();
    }

    // Update is called once per frame


    public void PositionAction()
    {
        if (isOpen)
        {
            iTweenArgs["position"] = closedPosition;
        }
        else
        {
            iTweenArgs["position"] = openPosition;
        }

        isOpen = !isOpen;

        iTween.MoveTo(this.gameObject, iTweenArgs);
    }

    public void RotationAction()
    {
  if (isOpen)
        {
            iTweenArgsRot["rotation"]  = Vector3.zero ;
        }

        isOpen = !isOpen;

        iTween.RotateTo(this.gameObject, iTweenArgsRot);


    }

    public void Collect()
    {
        if(isOpen == false)
        {
            isOpen = true;
            if(UITriggerEvent != null)
            {
                UITriggerEvent.panel.SetActive(true);
            }

            if (TriggerEvent != null)
            {
                TriggerEvent.CollectStory();
            }

            Destroy(gameObject);

        }
    }
}
