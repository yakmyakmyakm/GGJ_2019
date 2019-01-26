﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class MoveToClickPoint : MonoBehaviour 
{
        public Action<Vector3> onHitLocation;
        
        void Update() 
        {
            this.transform.rotation = Quaternion.Euler(Vector3.zero);

            if (Input.GetMouseButtonDown(0)) 
            {
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) 
                {
                    if(onHitLocation != null) onHitLocation(hit.point);
                }
            }
        }
    }