using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snoop : Interactable
{
    public Sprite sprite;

    private bool isActive;

    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }

    public Vector2 location; // (x,z) constrained to the floor

    public delegate int IntDelegate();

    public IntDelegate OnAwardSoulPoints;

    [SerializeField]
    private FloatVariable currentSnoopingDuration;

    [SerializeField]
    private GameEvent playerProgressBarStart;

    [SerializeField]
    private GameEvent playerProgressBarEnd;

    ProgressBar progressBar;

    public override Interactable.Type GetInteractableType()
    {
        return Type.Snoop;
    }

    public void StartSnoop()
    {
        isActive = true;

        //enable to endgame
        // if (MyFriend.Instance.IsPlayerCaught())
        // {
        //     //trigger endgame if the AI watches either start or end
        //     GameManager.instance.EndGame(true);
        // }


        //playImageSequence.StartAnimation(active);

        // if (currentSnoopingDuration) currentSnoopingDuration.SetValue(Duration);
        // if (playerProgressBarStart) playerProgressBarStart.Raise();

        if (progressBar == null) progressBar = this.transform.GetComponentInChildren<ProgressBar>(true);
        progressBar.Increase(Duration);
        progressBar.onCompleteIncrease = EndSnoop;
    }

    public void PlayerInterruptSnoop()
    {
        //progressBar.StopProgress();
        //playImageSequence.SetImage(active[0]);

        //if (playerProgressBarEnd) playerProgressBarEnd.Raise();
    }

    public void AIInterruptSnoop()
    {
        // XXX this should never happen
    }

    public virtual void EndSnoop()
    {
        //currentAnim = this.inactive;
        isActive = false;

        //enable to endgame
        // if (MyFriend.Instance.IsPlayerCaught())
        // {
        //     GameManager.instance.EndGame(true);
        // }

        // if (playImageSequence) playImageSequence.StartAnimation(inactive);
        // if (playerProgressBarEnd) playerProgressBarEnd.Raise();
    }

    void Start()
    {
        if (playImageSequence == null)
            playImageSequence = this.transform.GetComponentInChildren<PlayImageSequence>();
    
        playImageSequence.SetImage(sprite);
    }
}