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

    private List<Vector3> grid = new List<Vector3>();

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

            grid.Clear();
        this.genGrid();

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

    void genGrid() {
        for (int i = -xLimit; i <= xLimit; i++)
        {
            for (int j = -yLimit; j <= yLimit; j++)
            {
                if (i > -24 && j > -17) //exclude bed area
                {
                    this.grid.Add(new Vector3(i, 0, j));
                }
            }
        }

        this.grid.Shuffle();
    }

    Vector3 GetRandomPosition()
    {
        if(this.grid.Count == 0)
        {
            //do something, but this shouldn't happen
            this.genGrid();
        }
        Vector3 p = this.grid[this.grid.Count - 1];
        this.grid.RemoveAt(this.grid.Count - 1);
        return p;
    }
}

