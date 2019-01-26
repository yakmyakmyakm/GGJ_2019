using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Distraction
{
    public string name;

    public List<Sprite> inactive; // public for easy component editing
    public List<Sprite> active;

    private List<Sprite> currentAnim;

    public float duration; //  duration of distraction
    public Vector2 location; // (x,z) on the floor

    delegate void Handler();

    Handler OnStart = () => { };
    Handler OnEnd = () => { };

    public void StartDistraction()
    {
        currentAnim = active;
        this.OnStart();
        MyFriend.Instance.EnqueueDistraction(this);
    }

    public void EndDistraction ()
    {
        currentAnim = inactive;
        this.OnEnd();

    }

}
