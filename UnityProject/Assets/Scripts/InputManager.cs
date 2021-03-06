﻿using System.Collections;
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
    public Action onConversationClick;
    public Action onEvidenceMouseClick;
    public Action pause;
    public Action resume;

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

        switch (state)
        {
            case State.CONVERSATION:
                if (pause != null) pause();
                MyFriend.Instance.Pause();
                Time.timeScale = 0;
                break;
            case State.EVIDENCE:
                if (pause != null) pause();
                MyFriend.Instance.Pause();
                Time.timeScale = 0;
                break;
            case State.MOVEMENT:
                if (resume != null) resume();
                MyFriend.Instance.Resume();
                Time.timeScale = 1;
                break;
        }
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

                BroadcastTouchPoint();

                if (Input.GetMouseButtonUp(0))
                {
                    if (releasedMouseButton != null) releasedMouseButton();
                }

                if (Input.touchCount > 0)
                {
                    Touch t = Input.GetTouch(0);
                    if (t.phase == TouchPhase.Ended)
                    {
                        if (releasedMouseButton != null) releasedMouseButton();
                    }
                }

                break;

            case State.CONVERSATION:
                if (Input.GetMouseButtonUp(0))
                {
                    if (onConversationClick != null) onConversationClick();
                }

                if (Input.touchCount > 0)
                {
                    Touch t = Input.GetTouch(0);
                    if (t.phase == TouchPhase.Ended)
                    {
                        if (onConversationClick != null) onConversationClick();
                    }
                }

                break;

            case State.EVIDENCE:
                if (Input.GetMouseButtonUp(0))
                {
                    if (onEvidenceMouseClick != null) onEvidenceMouseClick();
                }

                if (Input.touchCount > 0)
                {
                    Touch t = Input.GetTouch(0);
                    if (t.phase == TouchPhase.Ended)
                    {
                        if (onEvidenceMouseClick != null) onEvidenceMouseClick();
                    }
                }


                break;
        }
    }

    public LayerMask layerMask;

    void Hit(RaycastHit hit)
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

    void BroadcastTouchPoint()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                // Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                // Plane plane = new Plane(Vector3.up, transform.position);
                // float distance = 0;
                // if (plane.Raycast(ray, out distance))
                // {
                //     Vector3 pos = ray.GetPoint(distance);
                // }
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit, 1000, layerMask))
                {
                    Hit(hit);
                }
            }
        }
    }

    void BroadcastHitPoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, layerMask))
        {
            Hit(hit);
        }
    }
}