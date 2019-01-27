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

    [SerializeField]
    private GameEvent AIProgressBarStart;

    [SerializeField]
    private GameEvent AIProgressBarStop;

    public override Interactable.Type GetInteractableType()
    {
        return Type.Distraction;
    }

    public virtual void StartPlayerDistraction()
    {
        Debug.Log("StartPlayerDistraction");
        currentAnim = active;
        this.OnStart();
        MyFriend.Instance.EnqueueDistraction(this);
        if (playImageSequence) playImageSequence.StartAnimationOnce(active);
        if (AIProgressBarStart) AIProgressBarStart.Raise();
    }

    public virtual void EndDistraction()
    {
        Debug.Log("EndDistraction");
        currentAnim = inactive;
        this.OnEnd();
        if (AIProgressBarStop) AIProgressBarStop.Raise();
    }

    //call this once AI has arrived
    public virtual void StartAIDistraction()
    {
        //playImageSequence.StartAnimation(active);
        //progressBar.onCompleteDecrease = EndAIDistraction;
    }

    public virtual void EndAIDistraction()
    {
        //playImageSequence.StartAnimation(inactive);
    }

    void Start()
    {
        Destination = this.transform.position;
        if (playImageSequence == null)
            playImageSequence = this.transform.GetComponentInChildren<PlayImageSequence>();
    }
}
