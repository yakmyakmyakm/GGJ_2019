﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g : Snoop
{
    public void StartSnoop()
    {
        base.StartSnoop();
    }

    public override void EndSnoop()
    {
        base.EndSnoop();
    }

    void Awake()
    {
        //name = "computer";
        Duration = 2.0f + Random.Range(0.0f, 6.0f);
    }
}
