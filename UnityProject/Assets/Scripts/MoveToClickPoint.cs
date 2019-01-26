using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToClickPoint : MonoBehaviour {
        NavMeshAgent agent;
        
        void Start() {
            agent = GetComponent<NavMeshAgent>();
        }
        
        void Update() {

            this.transform.rotation = Quaternion.Euler(Vector3.zero);

            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                    agent.destination = hit.point;
                }
            }
        }
    }