using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public PlayImageSequence playImageSequence;
    public float Duration;

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
