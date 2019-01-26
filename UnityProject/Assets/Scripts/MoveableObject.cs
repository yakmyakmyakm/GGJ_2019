using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveableObject : MonoBehaviour
{
    NavMeshAgent agent;
    public PlayImageSequence playImageSequence;
    public AnimationSprites animationSprites;
    Vector3 targetPosition;
    bool isMoving;

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
            if(Vector3.Distance(targetPosition, this.transform.position) <= 1)
            {
                isMoving = true;
                playImageSequence.image.sprite = animationSprites.idle;
                playImageSequence.StopAnimation();
            }
        }
    }

    public void MoveToPosition(Vector3 pos)
    {
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
        playImageSequence.StartAnimation(animationSprites.sideways);
    }

    void OnSwipeDown()
    {
        //right
        playImageSequence.StartAnimation(animationSprites.sideways);
    }

    void OnSwipeLeft()
    {
        //forward
        playImageSequence.StartAnimation(animationSprites.forward);
    }

    void OnSwipeRight()
    {
        //back
        playImageSequence.StartAnimation(animationSprites.back);
    }


}
