using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public PlayImageSequence playImageSequence;
    public ProgressBar progressBar;
    public float Duration;
    public List<Sprite> inactive; // public for easy component editing
    public List<Sprite> active;
    public List<Sprite> currentAnim;

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
