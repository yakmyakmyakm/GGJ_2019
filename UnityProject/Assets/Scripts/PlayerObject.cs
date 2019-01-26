using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    MoveToClickPoint moveToClickPoint;
    MoveableObject moveableObject;

    void Start()
    {
        moveToClickPoint = GetComponent<MoveToClickPoint>();
        moveableObject = GetComponent<MoveableObject>();

        moveToClickPoint.onHitLocation += MoveObject;
    }

    void MoveObject(Vector3 position)
    {
        moveableObject.MoveToPosition(position);
    }

    void Update()
    {
        
    }
}
