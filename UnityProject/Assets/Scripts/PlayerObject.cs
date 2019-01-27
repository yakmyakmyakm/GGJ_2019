using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public enum State
    {
        IDLE,
        SNOOPING,
        MOVING,
        DISTRACTING,
    }

    public State currentState;

    InputManager inputManager;
    MoveableObject moveableObject;

    GameObject target;
    Distraction currentDistraction;
    Snoop currentSnoop;
    bool isSnooping;

    Vector3 startingPos;

    private ProgressBar progressBar;

    public void Initalize()
    {
        currentState = State.IDLE;
        this.transform.position = startingPos;
    }

    void Start()
    {
        startingPos = this.transform.position;

        inputManager = GetComponent<InputManager>();
        moveableObject = GetComponent<MoveableObject>();

        inputManager.onHitLocation += MoveToPosition;
        inputManager.onHitDistraction += MoveToDistraction;
        inputManager.onHitSnoopable += MoveToSnoopable;
        inputManager.releasedMouseButton = OnMouseButtonUp;

        inputManager.pause = Pause;
        inputManager.resume = Resume;

        currentState = State.IDLE;
    }

    void Pause()
    {
        moveableObject.Pause();
    }

    void Resume()
    {
        moveableObject.Resume();
    }

    void MoveToDistraction(GameObject o)
    {
        if (currentState != State.DISTRACTING)
        {
            currentState = State.MOVING;
            target = o;
            moveableObject.onReachedDestination = ReachedDistractionDestination;
            moveableObject.MoveToPosition(o.transform.position);
        }
    }

    void ReachedDistractionDestination()
    {
        currentState = State.DISTRACTING;
        moveableObject.onReachedDestination = null;
        currentDistraction = target.GetComponent<Distraction>();
        if (currentDistraction.isActiveForPlayer)
        {
            currentDistraction.StartPlayerDistraction(); 
        }currentState = State.IDLE;

    }

    void MoveToSnoopable(GameObject o)
    {
        if (currentState != State.DISTRACTING)
        {
            currentState = State.MOVING;
            target = o;
            moveableObject.onReachedDestination = ReachedSnoopDestination;
            moveableObject.MoveToPosition(o.transform.position);
        }
    }

    void ReachedSnoopDestination()
    {
        currentState = State.SNOOPING;
        moveableObject.onReachedDestination = null;
        currentSnoop = target.GetComponent<Snoop>();
        if (currentSnoop.isActiveForPlayer)
        {
            currentSnoop.StartSnoop();
            Timer.RunTimer(currentSnoop.Duration, DoneSnooping);
        }
    }

    public void DoneSnooping()
    {
        currentState = State.IDLE;
        currentSnoop.EndSnoop();
    }

    void OnMouseButtonUp()
    {
        // if (currentState == State.SNOOPING)
        // {
        //     currentSnoop.PlayerInterruptSnoop();
        // }
    }

    void MoveToPosition(Vector3 position)
    {
        if (currentState != State.DISTRACTING && currentState != State.SNOOPING)
        {
            currentState = State.MOVING;
            moveableObject.MoveToPosition(position);
        }
    }
}
