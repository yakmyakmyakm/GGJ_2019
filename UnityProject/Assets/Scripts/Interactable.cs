using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public PlayImageSequence PlayImageSequence;
    public ProgressBar ProgressBar;
    public float Duration;
    public List<Sprite> Inactive; // public for easy component editing
    public List<Sprite> Active;
    public List<Sprite> CurrentAnim;

    public virtual Interactable.Type GetInteractableType()
    {
        return Type.Snoop;
    }

    public enum Type
    {
        Snoop,
        Distraction,
    }

    public virtual void OnStart()
    {

    }

    public virtual void OnEnd()
    {
        
    }
}
