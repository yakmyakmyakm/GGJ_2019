using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    InputManager inputManager;
    MoveableObject moveableObject;

    GameObject targetDistraction;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        moveableObject = GetComponent<MoveableObject>();
        

        inputManager.onHitLocation += MoveObject;
        inputManager.onHitDistraction += MoveToDistraction;

    }

    Distraction currentDistraction;
    
    void ReachedDestination()
    {
        currentDistraction = targetDistraction.GetComponent<Distraction>();
        currentDistraction.StartDistraction();
        currentDistraction.progressBar.Increase(currentDistraction.duration);
        currentDistraction.progressBar.onCompleteIncrease = DoneDistracting;
    }

    void DoneDistracting()
    {
        currentDistraction.EndDistraction();
    }

    void MoveToDistraction(GameObject o)
    {
        targetDistraction = o;
        moveableObject.onReachedDestination = ReachedDestination;
        moveableObject.MoveToPosition(o.transform.position);    
    }

    void MoveObject(Vector3 position)
    {
        moveableObject.MoveToPosition(position);
    }
}
