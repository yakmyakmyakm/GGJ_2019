using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    InputManager inputManager;
    MoveableObject moveableObject;

    GameObject target;
    Distraction currentDistraction;
    Snoop currentSnoop;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        moveableObject = GetComponent<MoveableObject>();


        inputManager.onHitLocation += MoveObject;
        inputManager.onHitDistraction += MoveToDistraction;
        inputManager.onHitSnoopable += MoveToSnoopable;

    }

    void MoveToDistraction(GameObject o)
    {
        target = o;
        moveableObject.onReachedDestination = ReachedDistractionDestination;
        moveableObject.MoveToPosition(o.transform.position);
    }

    void ReachedDistractionDestination()
    {
        currentDistraction = target.GetComponent<Distraction>();
        currentDistraction.StartDistraction();
        currentDistraction.progressBar.Increase(currentDistraction.duration);
        currentDistraction.progressBar.onCompleteIncrease = DoneDistracting;
    }

    void DoneDistracting()
    {
        currentDistraction.EndDistraction();
    }

    void MoveToSnoopable(GameObject o)
    {
        target = o;
        moveableObject.onReachedDestination = ReachedSnoopDestination;
        moveableObject.MoveToPosition(o.transform.position);
    }

    void ReachedSnoopDestination()
    {
        currentSnoop = target.GetComponent<Snoop>();
        currentSnoop.StartSnoop();
        currentSnoop.progressBar.Decrease(currentSnoop.Duration);
        currentSnoop.progressBar.onCompleteDecrease = DoneSnooping;
    }

    void DoneSnooping()
    {
        currentSnoop.EndSnoop();
    }

    void MoveObject(Vector3 position)
    {
        moveableObject.MoveToPosition(position);
    }
}
