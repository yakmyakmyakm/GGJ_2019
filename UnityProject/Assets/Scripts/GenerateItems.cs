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

    public int xLimit = 15;
    public int yLimit = 15;

    void Start()
    {
        if (snoops.Count > 0)
        {
            foreach (Snoop s in snoops)
            {
                s.gameObject.SetActive(false);
            }
        }

        if (distractions.Count > 0)
        {
            foreach (Distraction d in distractions)
            {
                d.gameObject.SetActive(false);
            }
        }

        snoops.Clear();
        distractions.Clear();
        snoopObjects.Shuffle();
        distractionObjects.Shuffle();

        int saftey = 0;
        for (int i = 0; i < snoopCount; i++)
        {
            if (i >= snoopObjects.Count)
            {
                saftey = 0;
            }
            Snoop snoop = Instantiate(snoopObjects[saftey], GetRandomPosition(), new Quaternion(0, 0, 0, 0));
            snoops.Add(snoop);
            saftey++;
        }

        saftey = 0;
        for (int i = 0; i < distractionCount; i++)
        {
            if (i >= distractionObjects.Count)
            {
                saftey = 0;
            }
            Distraction distraction = Instantiate(distractionObjects[saftey], GetRandomPosition(), new Quaternion(0, 0, 0, 0));
            distractions.Add(distraction);
        }
    }

    Vector3 GetRandomPosition()
    {
        int x = Random.Range(-xLimit, xLimit);
        int z = Random.Range(-yLimit, yLimit);
        return new Vector3(x, 0, z);
    }
}

