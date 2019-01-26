using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashcanSnoop : Snoop
{
    // public List<Sprite> inactive;
    // public List<Sprite> active;

    //public float duration = 1.0f;

    //private Snoop trashcan;

    public int OnAwardSoulPoints()
    {
        if (Random.Range(0.0f, 1.0f) < 0.5f)
        {
            return 20;
            // found a banana peel
        }
        else
        {
            return 50;
            // used condom or something
        }
    }

    void Awake()
    {
//         this.trashcan = new Snoop();
//         this.trashcan.active = this.active;
//         this.trashcan.inactive = this.inactive;
//         this.trashcan.name = "trashcan";
//         this.trashcan.duration = this.duration;
// //        Vector3 pos = GameObject.transform.position;
// //        this.trashcan.location = new Vector2(pos.x, pos.z);
//         this.trashcan.OnAwardSoulPoints = this.OnAwardSoulPoints;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
