using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snoop : Interactable
{
    // public string name;

    // public List<Sprite> inactive;
    // public List<Sprite> active;

    // private List<Sprite> currentAnim;
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

    public override Interactable.Type GetInteractableType()
    {
        return Type.Snoop;
    }

    public void StartSnoop()
    {
        currentAnim = this.active;
        isActive = true;

        // if(MyFriend.Instance.IsWatchful())
        // {
        //     //trigger endgame if the AI watches either start or end
        // }


        playImageSequence.StartAnimation(active);

        if (currentSnoopingDuration) currentSnoopingDuration.SetValue(Duration);
        if (playerProgressBarStart) playerProgressBarStart.Raise();
    }

    public void PlayerInterruptSnoop()
    {
        //progressBar.StopProgress();
        playImageSequence.SetImage(active[0]);

        if (playerProgressBarEnd) playerProgressBarEnd.Raise();
    }

    public void AIInterruptSnoop()
    {
        // XXX this should never happen
    }

    public virtual void EndSnoop()
    {
        currentAnim = this.inactive;
        isActive = false;
        // crc do delegate dance

        // if (MyFriend.Instance.IsWatchful())
        // {
        //     //trigger endgame if the AI watches either start or end
        // }

        if (playImageSequence) playImageSequence.StartAnimation(inactive);
        if (playerProgressBarEnd) playerProgressBarEnd.Raise();
    }

    void Start()
    {
        if (playImageSequence == null)
            playImageSequence = this.transform.GetComponentInChildren<PlayImageSequence>();
    }
}