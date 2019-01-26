﻿using System.Collections;
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

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        moveableObject = GetComponent<MoveableObject>();

        inputManager.onHitLocation += MoveToPosition;
        inputManager.onHitDistraction += MoveToDistraction;
        inputManager.onHitSnoopable += MoveToSnoopable;
        inputManager.releasedMouseButton = OnMouseButtonUp;

        currentState = State.IDLE;
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
        currentDistraction.StartPlayerDistraction();
        currentDistraction.progressBar.Increase(currentDistraction.duration);
        currentDistraction.progressBar.onCompleteIncrease = DoneDistracting;
    }

    void DoneDistracting()
    {
        currentState = State.IDLE;
        currentDistraction.EndPlayerDistraction();
        currentDistraction.progressBar.onCompleteIncrease = null;
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
        currentSnoop.StartSnoop();
        currentSnoop.progressBar.Decrease(currentSnoop.Duration);
        currentSnoop.progressBar.onCompleteDecrease = DoneSnooping;
    }

    void DoneSnooping()
    {
        currentState = State.IDLE;
        currentSnoop.EndSnoop();
        currentSnoop.progressBar.onCompleteDecrease = null;
    }

    void OnMouseButtonUp()
    {
        if (currentState == State.SNOOPING)
        {
            currentSnoop.PlayerInterruptSnoop();
        }
    }

    void MoveToPosition(Vector3 position)
    {
        if (currentState != State.DISTRACTING)
        {
            currentState = State.MOVING;
            moveableObject.MoveToPosition(position);
        }
    }
}
