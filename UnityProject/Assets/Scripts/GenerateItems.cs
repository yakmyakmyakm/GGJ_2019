using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItems : MonoBehaviour
{
    public Snoop bed;
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

        Snoop concreteBed = Instantiate(bed, new Vector3(-24, 0, 17), new Quaternion(0, 0, 0, 0));
        snoops.Add(concreteBed);
        concreteBed.transform.SetParent(s.transform);

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
        float xWallBonus = 15.0f; // How much more likely, relative to 27, something is to spawn against an x-wall
        float zWallPenalty  = 30.0f; // How much less likely, relative to 27, something is to spawn against a z-wall
        float x = Random.Range(-xLimit - xWallBonus, xLimit + xWallBonus);
        float z = Random.Range(-yLimit, yLimit + (2*zWallPenalty));
        if (x < -xLimit) { x = -xLimit; };
        if (x >  xLimit) { x = xLimit; };
        float zFactor = (yLimit - 6) / zWallPenalty; // increase magic number for more z centrality
        if (z < 0) { /*leave alone*/ }
        else if (z > 2*zWallPenalty) { z -= 2 * zWallPenalty; }
        else { z = (z * zFactor) - yLimit; } // remap onto the middle of z

        return new Vector3(x, 0, z);
    }
}

