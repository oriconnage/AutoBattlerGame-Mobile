using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_investigateharm : Ai_Interface
{
    private readonly StatePattern unit;
    public Ai_investigateharm (StatePattern state)
    {
        unit = state;
    }
    private float offset = 0.3f;
    private RaycastHit hit;
    private Vector3 lookAtTarget;
    public void UpdateState()
    {
        Look();
    }

    public void ToPatrolState()
    {
        unit.currentState = unit.patrolState;
    }

    public void ToAlertState()
    {
        unit.currentState = unit.alertState;
    }

    public void ToPursueState()
    {
        unit.currentState = unit.pursueState;
    }
    public void ToMeleeAttackState() { }
    public void ToRangeAttackState() { }

    void Look()
    {
        if (unit.pursueTarget == null)
        {
            ToPatrolState();
            return;
        }

        CheckIfTargetIsInDirectSight();
    }


    void CheckIfTargetIsInDirectSight()
    {
        lookAtTarget = new Vector3(unit.pursueTarget.position.x, unit.pursueTarget.position.y + offset, unit.pursueTarget.position.z);

        if (Physics.Linecast(unit.head.position, lookAtTarget, out hit, unit.sightLayers))
        {
            if (hit.transform.root == unit.pursueTarget)
            {
                unit.locationOfInterest = unit.pursueTarget.position;
                GoToLocationOfInterest();

                if (Vector3.Distance(unit.transform.position, lookAtTarget) <= unit.sightRange)
                {
                    ToPursueState();
                }
            }
            else
            {
                ToAlertState();
            }
        }
        else
        {
            ToAlertState();
        }
    }


    void GoToLocationOfInterest()
    {
        unit.meshRendererFlag.material.color = Color.black;

        if (unit.myNavMeshAgent.enabled && unit.locationOfInterest != Vector3.zero)
        {
            unit.myNavMeshAgent.SetDestination(unit.locationOfInterest);
            unit.myNavMeshAgent.isStopped = false;
            unit.AiMaster.CallEventUnitWalkAnim();

            if (unit.myNavMeshAgent.remainingDistance <= unit.myNavMeshAgent.stoppingDistance)
            {
                unit.locationOfInterest = Vector3.zero;
                ToPatrolState();
            }
        }

        else
        {
            ToPatrolState();
        }
    }

}
