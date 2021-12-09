using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Pursue :Ai_Interface
{
    private readonly StatePattern unit;
    private float caputredDistance;
    public Ai_Pursue(StatePattern state)
    {
        unit = state;
    }

    public void UpdateState()
    {
        Look();
        Pursue();
    }

    public void ToPatrolState()
    {
        KeepWalking();
        unit.currentState = unit.patrolState;
    }

    public void ToAlertState()
    {
        KeepWalking();
        unit.currentState = unit.alertState;
    }

    public void ToPursueState() { }
    public void ToMeleeAttackState()
    {
        unit.currentState = unit.meleeAttackState;
    }

    public void ToRangeAttackState() { unit.currentState = unit.rangeAttackState;}

    void Look()
    {
        if (unit.pursueTarget == null)
        {
            ToPatrolState();
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(unit.transform.position, unit.sightRange, unit.myEnemyLayers);

        if (colliders.Length == 0)
        {
            unit.pursueTarget = null;
            ToPatrolState();
            return;
        }

        caputredDistance = unit.sightRange * 2;

        foreach (Collider col in colliders)
        {
            float distanceToTarg = Vector3.Distance(unit.transform.position, col.transform.position);

            if (distanceToTarg < caputredDistance)
            {
                caputredDistance = distanceToTarg;
                unit.pursueTarget = col.transform.root;
            }
        }
    }

    void Pursue()
    {
        unit.meshRendererFlag.material.color = Color.red;

        if (unit.myNavMeshAgent.enabled && unit.pursueTarget != null)
        {
            unit.myNavMeshAgent.SetDestination(unit.pursueTarget.position);
            unit.locationOfInterest = unit.pursueTarget.position; //used by alert state
            KeepWalking();

            float distanceToTarget = Vector3.Distance(unit.transform.position, unit.pursueTarget.position);

            if (distanceToTarget <= unit.rangeAttackRange && distanceToTarget > unit.meleeAttackRange)
            {
                if (unit.hasRangeAttack)
                {
                    ToRangeAttackState();
                }
            }

            else if (distanceToTarget <= unit.meleeAttackRange)
            {
                if (unit.hasMeleeAttack)
                {
                    ToMeleeAttackState();
                }
                else if (unit.hasRangeAttack)
                {
                    ToRangeAttackState();
                }
            }
        }

        else
        {
            ToAlertState();
        }
    }

    void KeepWalking()
    {
        if (unit.myNavMeshAgent.enabled)
        {
            unit.myNavMeshAgent.isStopped = false;
            unit.AiMaster.CallEventUnitWalkAnim();
        }
    }
}
