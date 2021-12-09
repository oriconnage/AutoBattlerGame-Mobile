using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Struck : Ai_Interface
{
    private readonly StatePattern unit;
    private float informRate = 0.5f;
    private float nextInform;
    private Collider[] colliders;
    private Collider[] friendlyColliders;
    public Ai_Struck(StatePattern state)
    {
        unit = state;
    }
    public void UpdateState()
    {
        InformNearbyAlliesThatIHaveBeenHurt();
    }
    public void ToPatrolState() { }

    public void ToAlertState()
    {
        //npc.currentState = npc.alertState;
    }
    public void ToPursueState() { }
    public void ToMeleeAttackState() { }
    public void ToRangeAttackState() { }

    void InformNearbyAlliesThatIHaveBeenHurt()
    {
        if (Time.time > nextInform)
        {
            nextInform = Time.time + informRate;
        }
        else
        {
            return;
        }

        if (unit.myAttacker != null)
        {
            friendlyColliders = Physics.OverlapSphere(unit.transform.position, unit.sightRange, unit.myFriendlyLayers);

            if (IsAttackerClose())
            {
                AlertNearbyAllies();
                SetMyselfToInvestigate();
            }
        }
    }

    bool IsAttackerClose()
    {
        if (Vector3.Distance(unit.transform.position, unit.myAttacker.position) <= unit.sightRange * 2)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    void AlertNearbyAllies()
    {
        foreach (Collider ally in friendlyColliders)
        {
            if (ally.transform.root.GetComponent<StatePattern>() != null)
            {
                StatePattern allyPattern = ally.transform.root.GetComponent<StatePattern>();

                if (allyPattern.currentState == allyPattern.patrolState)
                {
                    allyPattern.pursueTarget = unit.myAttacker;
                    allyPattern.locationOfInterest = unit.myAttacker.position;
                    allyPattern.currentState = allyPattern.investigateHarmState;
                    allyPattern.AiMaster.CallEventUnitWalkAnim();
                }
            }
        }
    }


    void SetMyselfToInvestigate()
    {
        unit.pursueTarget = unit.myAttacker;
        unit.locationOfInterest = unit.myAttacker.position;

        if (unit.capturedState == unit.patrolState)
        {
            unit.capturedState = unit.investigateHarmState;
        }
    }
}
