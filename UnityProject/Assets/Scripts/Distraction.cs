using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Distraction : Interactable
{
    public Vector3 Destination;
    public string name;
    delegate void Handler();

    Handler OnStart = () => { };
    Handler OnEnd = () => { };

    public override Interactable.Type GetInteractableType()
    {
        return Type.Distraction;
    }

    public virtual void StartPlayerDistraction()
    {
        Debug.Log("start distraction");
        currentAnim = active;
        this.OnStart();
        MyFriend.Instance.EnqueueDistraction(this);
        playImageSequence.StartAnimationOnce(active);
    }

    public virtual void EndDistraction()
    {
        currentAnim = inactive;
        this.OnEnd();
    }

    //call this once AI has arrived
    public virtual void StartAIDistraction()
    {
        //playImageSequence.StartAnimation(active);
        progressBar.Decrease(Duration);
        //progressBar.onCompleteDecrease = EndAIDistraction;
    }

    public virtual void EndAIDistraction()
    {
        //playImageSequence.StartAnimation(inactive);
    }

    void Start()
    {
        Destination = this.transform.position;
    }
}
