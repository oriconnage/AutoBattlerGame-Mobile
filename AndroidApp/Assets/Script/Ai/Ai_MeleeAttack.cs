using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_MeleeAttack : Ai_Interface {

    private readonly StatePattern unit;
    private float distanceToTarget;

    public Ai_MeleeAttack(StatePattern UnitStatePattern)
    {
        unit = UnitStatePattern;
    }

    public void UpdateState()
    {
        Look();
        TryAttack();
    }

    public void ToPatrolState()
    {
        keepWalking();
        unit.isMeleeAttacking = false;
        unit.currentState = unit.patrolState;
    }

    public void ToAlertState()
    {
        keepWalking();
        unit.isMeleeAttacking = false;
        unit.currentState = unit.alertState;
    }

   public void ToPursueState()
    {
        keepWalking();
        unit.isMeleeAttacking = false;
        unit.currentState = unit.pursueState;
    }

   public void ToMeleeAttackState()
    {
       
    }

    public void ToRangeAttackState()
    {
        
    }


    public void Look()
    {
        if(unit.pursueTarget == null)
        {
            ToPursueState();
            return;
        }
        Collider[] collider = Physics.OverlapSphere(unit.transform.position, unit.meleeAttackRange, unit.myEnemyLayers);

        if(collider.Length ==0)
        {
            ToPursueState();
            return;
        }
        foreach (Collider col in collider)
        {
           if(col.transform.root == unit.pursueTarget)
            {
                return;
            }
        }
        ToPursueState();
    }
   public void TryAttack()
    {
        if(unit.pursueTarget != null)
        {
            //Debug.Log("In attack state");
            unit.meshRendererFlag.material.color = Color.magenta;
            if(Time.time > unit.nextAttack && !unit.isMeleeAttacking)
            {
                unit.nextAttack = Time.time + unit.attackRate;
                if (Vector3.Distance(unit.transform.position, unit.pursueTarget.position) < unit.meleeAttackRange)
                {
                    Vector3 NewPos = new Vector3(unit.pursueTarget.position.x, unit.transform.position.y, unit.pursueTarget.position.z);
                    unit.transform.LookAt(NewPos);
                    unit.AiMaster.CallEventUnitAttackAnim();
                    unit.isMeleeAttacking = true;
                }
                else
                {
                    ToPursueState();
                }
            }
        }
        else
        {
            ToPursueState();
        }
    }

    public void keepWalking()
    {
        if (unit.myNavMeshAgent.enabled)
        {
            unit.myNavMeshAgent.isStopped = false;
            unit.AiMaster.CallEventUnitWalkAnim();
        }
    }

 
}
