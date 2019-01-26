﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenDistraction : MonoBehaviour
{
    public List<Sprite> inactive;
    public List<Sprite> active;

    public float duration;

    private Distraction kitten;

    void Awake()
    {
        this.kitten = new Distraction();
        this.kitten.active = this.active;
        this.kitten.inactive = this.inactive;
        this.kitten.name = "kitten";
        this.kitten.duration =  4.0f + Random.Range(0.0f, 4.0f);
        //Vector3 pos = GameObject.transform.position;
        //this.kitten.location = new Vector2(pos.x, pos.z);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void BeginDistraction()
    {
        this.kitten.StartDistraction();
        MyFriend.Instance.EnqueueDistraction(this.kitten);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
