using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class MoveableObject : MonoBehaviour
{
    NavMeshAgent agent;
    public PlayImageSequence playImageSequence;
    public AnimationSprites animationSprites;
    Vector3 targetPosition;
    bool isMoving;
    MovementDirection movementDirection;

    public Action onReachedDestination;

    public enum State
    {
        Idle,
        Moving,
    }

    public enum MovementDirection
    {
        Forward,
        Back,
        Left,
        Right
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(isMoving)
        {
            if(Vector3.Distance(targetPosition, this.transform.position) <= 3)
            {
                agent.isStopped = true;
                isMoving = false;
                playImageSequence.StopAnimation();
                if (onReachedDestination != null) onReachedDestination();
            }
        }
    }

    public void MoveToPosition(Vector3 pos)
    {
        agent.isStopped = false;
        isMoving = true;
        targetPosition = pos;
        Vector3 heading = (pos - this.transform.position).normalized;
        var distanceToTarget = heading.magnitude;
        var direction = heading / distanceToTarget;

        startPos = new Vector2(this.transform.position.x, this.transform.position.z);
        endPos = new Vector2(pos.x, pos.z);

        playImageSequence.StopAnimation();
        checkSwipe();
        agent.destination = pos;
    }

    public void Pause()
    {
        agent.isStopped = true;
    }

    public void Resume()
    {
        agent.isStopped = false;
    }

    private Vector2 startPos;
    private Vector2 endPos;

    void checkSwipe()
    {
        if (verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (startPos.y - endPos.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (startPos.y - endPos.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            endPos = startPos;
        }

        else if (horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (startPos.x - endPos.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (startPos.x - endPos.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            endPos = startPos;
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(startPos.y - endPos.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(startPos.x - endPos.x);
    }

    void OnSwipeUp()
    {
        //left
        MoveInDirection(MovementDirection.Left);
    }

    void OnSwipeDown()
    {
        //right
        MoveInDirection(MovementDirection.Right);
    }

    void OnSwipeLeft()
    {
        //forward
        MoveInDirection(MovementDirection.Forward);
    }

    void OnSwipeRight()
    {
       MoveInDirection(MovementDirection.Back);
    }

    void MoveInDirection(MovementDirection direction)
    {
        switch(direction)
        {
            case MovementDirection.Left: playImageSequence.StartAnimation(animationSprites.sideways, true);break;
            case MovementDirection.Right: playImageSequence.StartAnimation(animationSprites.sideways);break;
            case MovementDirection.Forward:  playImageSequence.StartAnimation(animationSprites.forward);break;
            case MovementDirection.Back: playImageSequence.StartAnimation(animationSprites.back); break;
        }
    }
}
