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

    //public float duration; // time required, in seconds, to inspect

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
        // crc do delegate dance

        playImageSequence.StartAnimation(active);
    }

    public void PlayerInterruptSnoop()
    {
        progressBar.StopProgress();
        playImageSequence.SetImage(active[0]);
    }

    public void AIInterruptSnoop()
    {

    }

    public void EndSnoop()
    {
        currentAnim = this.inactive;
        isActive = false;
        // crc do delegate dance

        playImageSequence.StartAnimation(inactive);
    }
}