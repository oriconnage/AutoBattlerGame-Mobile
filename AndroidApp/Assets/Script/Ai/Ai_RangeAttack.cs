using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_RangeAttack : Ai_Interface
{
    private readonly StatePattern unit;
    private RaycastHit hit;

    public Ai_RangeAttack(StatePattern state)
    {
        unit = state;
    }
   public void UpdateState()
    {
        Look();
        TryAttack();
    }
   public void ToPatrolState()
    {
        keepWalking();
        unit.pursueTarget = null;
        unit.currentState = unit.patrolState;
    }
    public void ToAlertState()
    {
        keepWalking();
        unit.currentState = unit.alertState;
    }
    public void ToPursueState()
    {
        keepWalking();
        unit.currentState = unit.pursueState;
    }
    public void ToMeleeAttackState()
    {
        unit.currentState = unit.meleeAttackState;
    }
    public void ToRangeAttackState()
    {

    }
    public void Look()
    {
        if (unit.pursueTarget == null)
        {
            ToPatrolState();
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(unit.transform.position, unit.sightRange, unit.myEnemyLayers);

        if (colliders.Length == 0)
        {
            ToPatrolState();
            return;
        }

        foreach (Collider col in colliders)
        {
            if (col.transform.root == unit.pursueTarget)
            {
                LookTowardTarget();
                return;
            }
        }

        ToPatrolState();
    }
    public void TryAttack()
    {
        if (unit.pursueTarget != null)
        {
            unit.meshRendererFlag.material.color = Color.cyan;

            if (!IsTargetInSight())
            {
                ToPursueState();
                return;
            }

            if (Time.time > unit.nextAttack)
            {
                unit.nextAttack = Time.time + unit.attackRate;

                float distanceToTarget = Vector3.Distance(unit.transform.position, unit.pursueTarget.position);

                LookTowardTarget();

                // target is in range!
                if (distanceToTarget <= unit.rangeAttackRange)
                {
                    stopWalking();
                    // add range attack here
                    if (unit.rangeWeapon.GetComponent<Ai_RangeWeponManager>() != null)
                    {
                        //Debug.Log("range attacking");
                        unit.rangeWeapon.GetComponent<Ai_RangeWeponManager>().CallEventAiInput(unit.rangeAttackSpread);
                        unit.AiMaster.CallEventUnitAttackAnim();
                        return;
                    }
                }
                if (distanceToTarget <= unit.meleeAttackRange && unit.hasMeleeAttack)
                {
                    ToMeleeAttackState();
                }
            }
        }

        else
        {
            ToPatrolState();
        }
    }

    public void LookTowardTarget()
    {
        Vector3 newPos = new Vector3(unit.pursueTarget.position.x, unit.transform.position.y, unit.pursueTarget.position.z);
        unit.transform.LookAt(newPos);
    }
    public void keepWalking()
    {
        if (unit.myNavMeshAgent.enabled)
        {
            unit.myNavMeshAgent.isStopped = false;
            unit.AiMaster.CallEventUnitWalkAnim();
        }
    }
    public void stopWalking()
    {
        if (unit.myNavMeshAgent.enabled)
        {
            unit.myNavMeshAgent.isStopped = true;
            unit.AiMaster.CallEventUnitIdleAnim();
        }
    }

    bool IsTargetInSight(){
        RaycastHit hit;

        Vector3 weaponLookAtVector = new Vector3(unit.pursueTarget.position.x, unit.pursueTarget.position.y + unit.offset, unit.pursueTarget.position.z);
        unit.rangeWeapon.transform.LookAt(weaponLookAtVector);

        if (Physics.Raycast(unit.rangeWeapon.transform.position, unit.rangeWeapon.transform.forward, out hit))
        {
            foreach (string tag in unit.myEnemyTags)
            {
                if (hit.transform.root.CompareTag(tag))
                {
                    return true;
                }
            }
            return false;
        }

        else
        {
            return false;
        }
    }
}


