using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class InputManager : MonoBehaviour
{
    public Action<Vector3> onHitLocation;
    public Action<GameObject> onHitDistraction;
    public Action<GameObject> onHitSnoopable;
    public Action releasedMouseButton;
    public Action onMouseClick;

    public enum State
    {
        MOVEMENT,
        CONVERSATION,
        EVIDENCE,
    }

    State currentState = State.MOVEMENT;
    public void SetState(State state)
    {
        currentState = state;
    }


    void Update()
    {
        this.transform.rotation = Quaternion.Euler(Vector3.zero);

        switch (currentState)
        {
            case State.MOVEMENT:
                if (Input.GetMouseButtonDown(0))
                {
                    BroadcastHitPoint();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (releasedMouseButton != null) releasedMouseButton();
                }
                break;
            case State.CONVERSATION:
            case State.EVIDENCE:
                BroadcastMouseClick();
                break;
        }
    }

    void BroadcastMouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (onMouseClick != null) onMouseClick();
        }
    }

    void BroadcastHitPoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            if (hit.transform.CompareTag("Distraction"))
            {
                if (onHitDistraction != null) onHitDistraction(hit.transform.gameObject);
            }
            else if (hit.transform.CompareTag("Snoopable"))
            {
                if (onHitSnoopable != null) onHitSnoopable(hit.transform.gameObject);
            }
            else
            {
                if (onHitLocation != null) onHitLocation(hit.point);
            }
        }
    }
}