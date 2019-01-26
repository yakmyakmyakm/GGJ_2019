using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenDistraction : Distraction
{
    void Awake()
    {
        // this.kitten = new Distraction();
        // this.kitten.active = this.active;
        // this.kitten.inactive = this.inactive;
        // this.kitten
        // this.kitten
        //Vector3 pos = GameObject.transform.position;
        //this.kitten.location = new Vector2(pos.x, pos.z);

        name = "kitten";
        duration =  4.0f + Random.Range(0.0f, 4.0f);
    }

    public override void StartPlayerDistraction()
    {
        base.StartPlayerDistraction();
        Debug.Log("kitten distractin");
    }

     public override void EndPlayerDistraction()
    {
        base.EndPlayerDistraction();
        Debug.Log("complete kitten distractin");
    }

    // void BeginDistraction()
    // {
    //     this.kitten.StartDistraction();
    //     MyFriend.Instance.EnqueueDistraction(this.kitten);
}
