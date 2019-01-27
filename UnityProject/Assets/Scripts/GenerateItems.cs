using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItems : MonoBehaviour
{
    public List<Snoop> snoopObjects;
    public List<Distraction> distractionObjects;
    public int snoopCount;
    public int distractionCount;

    public List<Distraction> distractions;
    public List<Snoop> snoops;

    void Start()
    {
        for (int i = 0; i < snoopCount; i++)
        {

        }

        for (int i = 0; i < distractionCount; i++)
        {

        }
    }

    // Snoop GenerateSnoop()
    // {
    //     Snoop snoop = Instantiate()
    // }

    Vector2 GetRandomPosition()
    {
        return Vector2.zero;
    }



}
