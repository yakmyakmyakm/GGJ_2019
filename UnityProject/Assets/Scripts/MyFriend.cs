﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// todo: forward declaration. can be removed later
public partial class Player
{
    public static Player Instance;
    public Transform transform;
}

public class MyFriend : MonoBehaviour
{
    // Make it a singleton
    public static MyFriend Instance = null;

    public enum State {
        WATCHFUL,       // (start) look around
        APPROACH,       // go near the player
        CATCH,          // catch the player in action
        BE_DISTRACTED,  // be distracted by something 
    };

    // Keeps a distraction queue
    private Queue<Distraction> distractionQueue = new Queue<Distraction>();

    private State lastState;

    [SerializeField]
    private State currentState;

    private State nextState;
    
    private bool catchSnooping;  // set by a callback function

    private bool distractionStarted; 

    private Action OnDistractionTimeOut;

    private MoveableObject moveableObject;

    private Vector3 destination;

    [SerializeField]
    private GameObject player;

    // todo: mock
    [SerializeField]
    private Distraction sendInDistraction;

    [SerializeField]
    private float nearDistance = 1.0f; // when the player is near, stop approach

    [SerializeField]
    private float farDistance = 4.0f; // when the player is far away, approach

    [SerializeField]
    private float reachDistance = 1.0f; // when the player reaches a position
    
    // Add distraction to the end of queue
    // Called by the Distraction
    public void EnqueueDistraction(Distraction inDistraction)
    {
        Debug.Log("EnqueueDistraction");
        distractionQueue.Enqueue(inDistraction);
        moveableObject.MoveToPosition(inDistraction.Destination);
    }

    public bool IsWatchful()
    {
        return currentState == State.WATCHFUL;
    }

    // Get the first distraction from the queue
    private Distraction GetDistraction()
    {
        if (IsDistracted())
        {
            return distractionQueue.Peek();
        }
        return null;
    }

    private void RemoveDistraction()
    {
        Debug.Log("RemoveDistraction");
        distractionStarted = false;
        OnDistractionTimeOut -= RemoveDistraction;
        if (IsDistracted())
        {
            // send end event to the distraction and remove it from the queue
            GetDistraction().EndDistraction();
            distractionQueue.Dequeue();
        }
    }

    // Initialize
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        moveableObject = GetComponent<MoveableObject>();
    }

    // Update the state machine
    private void Update()
    {
        if (currentState == State.WATCHFUL)
        {
            // WATCHFUL state: not move, just look around
            UpdateWatchful();

            // go to CATCH state if it catches the snooping action
            if (catchSnooping)
            {
                nextState = State.CATCH;  // todo: end game
            }

            // go to BE_DISTRACTED if the distraction queue is not empty
            if (IsDistracted())
            {
                nextState = State.BE_DISTRACTED;
            }

            // go to APPROACH state if it is far
            if (IsPlayerFar())
            {
                nextState = State.APPROACH;
            }
        }
        else if (currentState == State.APPROACH)
        {
            UpdateApproach();

            // go to BE_DISTRACTED if the distraction queue is not empty
            if (IsDistracted())
            {
                nextState = State.BE_DISTRACTED;
            }

            // go to WATCHFUL state when the player is near
            if (IsPlayerNear())
            {
                nextState = State.WATCHFUL;
            }
        }
        else if (currentState == State.BE_DISTRACTED)
        {
            UpdateBeDistracted();

            // go to WATCHGUL state when the distraction queue is empty
            if (IsDistracted() == false)
            {
                nextState = State.WATCHFUL;
            }
        }
    }

    private void LateUpdate()
    {
        lastState = currentState;
        currentState = nextState;

        UpdateInput();
    }

    private void UpdateInput()
    {
        if (Input.GetButtonDown("Watchful"))
        {
            nextState = State.WATCHFUL;
        }
        else if (Input.GetButtonDown("Be Distracted"))
        {
            // mock
            EnqueueDistraction(sendInDistraction);
        }
        else if (Input.GetButtonDown("Approach"))
        {
            nextState = State.APPROACH;
        }
        else if (Input.GetButtonDown("Catch"))
        {
            nextState = State.CATCH;
        }
    }

    private void Initialize()
    {
        // initialize states
        currentState = State.WATCHFUL;
        lastState = State.WATCHFUL;
        nextState = State.WATCHFUL;

        // clear queue
        distractionQueue.Clear();

        catchSnooping = false;
        distractionStarted = false;

        destination = transform.position;
    }

    private bool IsPlayerFar()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance > farDistance;
    }

    private bool IsPlayerFar(Vector3 position)
    {
        float distance = Vector3.Distance(player.transform.position, position);
        return distance > farDistance;
    }

    private bool IsPlayerNear()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance < nearDistance;
    }

    private bool IsReached(Vector3 Position)
    {
        float distance = Vector3.Distance(Position, transform.position);
        return distance < reachDistance;
    }

    private bool IsDistracted()
    {
        return distractionQueue.Count > 0;
    }

    private void UpdateWatchful()
    {
        // nothing happens
    }

    private void UpdateApproach()
    {
        if (IsPlayerFar(destination))
        {
            destination = player.transform.position;
            moveableObject.MoveToPosition(destination);
        }
    }

    private void UpdateBeDistracted()
    {
        if (GetDistraction() == null)
        {
            // early return
            return;
        }

        if (IsReached(GetDistraction().Destination) && distractionStarted == false)
        {
            // start timer, when the time is out, call remove distraction
            Debug.Log("UpdateBeDistracted start timer for " + GetDistraction().Duration + " s");
            destination = transform.position;
            distractionStarted = true;
            OnDistractionTimeOut -= RemoveDistraction;
            OnDistractionTimeOut += RemoveDistraction;
            Timer.RunTimer(GetDistraction().Duration, OnDistractionTimeOut);
        }
    }
}
