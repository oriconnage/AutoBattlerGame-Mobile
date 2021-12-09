using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Alert : Ai_Interface
{
    private readonly StatePattern unit;
    private float informRate = 3;
    private float nextInform;
    private float offset = 0.3f;
    private Vector3 targetPosition;
    private RaycastHit hit;
    private Collider[] colliders;
    private Collider[] friendlyColliders;
    private Vector3 lookAtTarget;
    private int detectionCount;
    private int lastDetectionCount;
    private Transform possibleTarget;
    public Ai_Alert(StatePattern state)
    {
        unit = state;
    }

    public void UpdateState()
    {
        Look();
    }
    public void ToPatrolState()
    {
        unit.currentState = unit.patrolState;
    }
    public void ToAlertState() { }
    public void ToPursueState()
    {
        unit.currentState = unit.pursueState;
      //  Debug.Log(" going to the pursue");
    }
    public void ToMeleeAttackState() { }
    public void ToRangeAttackState() { }

    void Look()
    {
        colliders = Physics.OverlapSphere(unit.transform.position, unit.sightRange, unit.myEnemyLayers);

        foreach (Collider col in colliders)
        {
            lookAtTarget = new Vector3(col.transform.position.x, col.transform.position.y + offset, col.transform.position.z);

            if (Physics.Linecast(unit.head.position, lookAtTarget, out hit, unit.sightLayers))
            {
                foreach (string tags in unit.myEnemyTags)
                {
                    if (hit.transform.CompareTag(tags))
                    {
                        detectionCount++;
                        possibleTarget = col.transform;
                      //  Debug.Log(" Add detection : "+detectionCount.ToString());
                        break;
                    }
                }
            }
        }
        //Check if detection count is greater than 0 and if so pursue
        if (detectionCount > 0)
        {
           // Debug.Log("D -> pursue " + detectionCount);
            
            detectionCount = 0;
            unit.locationOfInterest = possibleTarget.position;
            unit.pursueTarget = possibleTarget.root;
            InformNearbyAllies();
            ToPursueState();
            // ToPursueState();
        }


        GoToLocationOfInterest();
    }

    void GoToLocationOfInterest()
    {
        unit.meshRendererFlag.material.color = Color.yellow;

        if (unit.myNavMeshAgent.enabled && unit.locationOfInterest != Vector3.zero)
        {
            unit.myNavMeshAgent.SetDestination(unit.locationOfInterest);
            unit.myNavMeshAgent.isStopped = false;
            unit.AiMaster.CallEventUnitWalkAnim();

            if (unit.myNavMeshAgent.remainingDistance <= unit.myNavMeshAgent.stoppingDistance &&
           !unit.myNavMeshAgent.pathPending)
            {
                unit.AiMaster.CallEventUnitIdleAnim();
                unit.locationOfInterest = Vector3.zero;
                ToPatrolState();
            }
        }
    }

    void InformNearbyAllies()
    {
        if (Time.time > nextInform)
        {
            nextInform = Time.time + informRate;

            friendlyColliders = Physics.OverlapSphere(unit.transform.position, unit.sightRange, unit.myFriendlyLayers);

            if (friendlyColliders.Length == 0)
            {
                return;
            }

            foreach (Collider ally in friendlyColliders)
            {
                if (ally.transform.root.GetComponent<StatePattern>() != null)
                {
                    StatePattern allyPattern = ally.transform.root.GetComponent<StatePattern>();

                    if (allyPattern.currentState == allyPattern.patrolState)
                    {
                        allyPattern.pursueTarget = unit.pursueTarget;
                        allyPattern.locationOfInterest = unit.pursueTarget.position;
                        allyPattern.currentState = allyPattern.alertState;
                        allyPattern.AiMaster.CallEventUnitWalkAnim();
                    }
                }
            }
        }
    }

}

