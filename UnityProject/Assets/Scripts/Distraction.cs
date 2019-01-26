using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Distraction : Interactable
{

    public Vector3 Destination;
    public string name;
    public Vector2 location; // (x,z) on the floor

    delegate void Handler();

    Handler OnStart = () => { };
    Handler OnEnd = () => { };

    public override Interactable.Type GetInteractableType()
    {
        return Type.Distraction;
    }

    public virtual void StartPlayerDistraction()
    {
        currentAnim = active;
        this.OnStart();
        //MyFriend.Instance.EnqueueDistraction(this);

         playImageSequence.StartAnimationOnce(active);
    }

    public virtual void EndDistraction()
    {
        currentAnim = inactive;
        this.OnEnd();
    }

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
}
