using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItems : MonoBehaviour
{
    public List<Snoop> snoopObjects;
    public List<Distraction> distractionObjects;
    public int snoopCount;
    public int distractionCount;

    private List<Distraction> distractions = new List<Distraction>();
    private List<Snoop> snoops = new List<Snoop>();

    public int xLimit = 15;
    public int yLimit = 15;

    GameObject s;
    GameObject d;

    void Start()
    {
        InitalizeWorld();
    }

    public void InitalizeWorld()
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

        if(s == null) s = new GameObject("Snoops");
        if(d == null) d = new GameObject("Distractions");

        int saftey = 0;
        for (int i = 0; i < snoopCount; i++)
        {
            if (i >= snoopObjects.Count)
            {
                saftey = 0;
            }
            Snoop snoop = Instantiate(snoopObjects[saftey], GetRandomPosition(), new Quaternion(0, 0, 0, 0));
            snoops.Add(snoop);
            snoop.transform.SetParent(s.transform);
            saftey++;
        }

        saftey = 0;
        for (int i = 0; i < distractionCount; i++)
        {
            if (i >= distractionObjects.Count)
            {
                saftey = 0;
            }

            //Debug.Log("SAFTEY "  +saftey + " objcount " + distractionObjects.Count + " " + i);
            Distraction distraction = Instantiate(distractionObjects[saftey], GetRandomPosition(), new Quaternion(0, 0, 0, 0));
            distractions.Add(distraction);
            distraction.transform.SetParent(d.transform);
            saftey++;
        }
    }

    Vector3 GetRandomPosition()
    {
        int x = Random.Range(-xLimit, xLimit);
        int z = Random.Range(-yLimit, yLimit);
        return new Vector3(x, 0, z);
    }
}

