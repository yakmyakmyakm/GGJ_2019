using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Distraction
{
    public Start();

    public string name;

    public float duration; // fixed duration of distraction
    public float durationRandom; // uniform distribution, added to duration

    private bool isActive;

    public bool IsActive
    {
        get
        {
            return this.isActive;
        }
    }

    public UnityEvent end;

}
