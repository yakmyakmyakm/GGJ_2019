using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snoop
{
    public string name;

    public List<Sprite> inactive;
    public List<Sprite> active;

    private List<Sprite> currentAnim;
    private bool isActive;

    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }

    public float duration; // time required, in seconds, to inspect

    public Vector2 location; // (x,z) constrained to the floor

    public delegate int IntDelegate();

    public IntDelegate OnAwardSoulPoints;

    public void StartDistraction()
    {
        currentAnim = this.active;
        isActive = true;
        // crc do delegate dance
    }

    public void EndDistraction()
    {
        currentAnim = this.inactive;
        isActive = false;
        // crc do delegate dance
    }
}