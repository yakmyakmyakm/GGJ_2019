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
    }

    public void PlayerInterruptSnoop()
    {
        progressBar.StopProgress();
        playImageSequence.SetImage(active[0]);
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

        playImageSequence.StartAnimation(inactive);
    }
}