using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: forward declaration. can be removed later
public partial class Distraction
{
    public float Duration;
    public Vector3 Destination;
}

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
    private Queue<Distraction> distractionQueue;

    private State lastState;

    private State currentState;

    private State nextState;

    private bool activeSnoop;  // set by a callback function

    private float distractionTimer;  // start timer when a distraciton starts. 
    // end when it reaches the distractio duration

    private Distraction currentDistraction;

    [SerializeField]
    private float nearDistance = 1.0f; // when the player is near, stop approach

    [SerializeField]
    private float farDistance = 4.0f; // when the player is far away, approach

    [SerializeField]
    private float reachDistance = 1.0f; // when the player reaches a position

    // Add distraction to the end of queue
    public void EnqueueDistraction(Distraction inDistraction)
    {
        distractionQueue.Enqueue(inDistraction);
    }

    // Get the first distraction from the queue
    private Distraction GetDistraction()
    {
        return distractionQueue.Peek();
    }

    private void RemoveDistraction()
    {
        distractionQueue.Dequeue();
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
        
    }

    // Update the state machine
    private void Update()
    {
        if (currentState == State.WATCHFUL)
        {
            // WATCHFUL state: not move, just look around
            UpdateWatchful();

            // go to CATCH state if it sees the action
            if (activeSnoop)
            {
                nextState = State.CATCH;  // todo: end game
            }

            // go to NOT_WATCHFUL if the distraction queue is not empty
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
    }

    private void Initialize()
    {
        // initialize states
        currentState = State.WATCHFUL;
        lastState = State.WATCHFUL;
        nextState = State.WATCHFUL;

        // clear queue
        distractionQueue.Clear();

        activeSnoop = false;
        distractionTimer = 0;
    }

    private bool IsPlayerFar()
    {
        float distance = Vector3.Distance(Player.Instance.transform.position, transform.position);
        return distance > farDistance;
    }

    private bool IsPlayerNear()
    {
        float distance = Vector3.Distance(Player.Instance.transform.position, transform.position);
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
        // todo: look around
    }

    private void UpdateApproach()
    {
        if (lastState != currentState)
        {
            // todo: use the nav mesh to move to destination
        }
    }

    private void UpdateBeDistracted()
    {
        if (lastState != currentState)
        {
            currentDistraction = GetDistraction();
            // todo: use the nav mesh to move to destination
            return;
        }

        if (IsReached(currentDistraction.Destination))
        {
            distractionTimer = 0;
        }

        // wait for a duration, and remove the distraction from the queue
        distractionTimer += Time.deltaTime;
        if (distractionTimer >= currentDistraction.Duration)
        {
            RemoveDistraction();
        }
    }
}
