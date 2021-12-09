using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_Patrol : Ai_Interface
{
    private readonly StatePattern unitstate;
    private int nextPoint;
    private Collider[] colliders;
    private Vector3 lookAtPoint;
    private Vector3 movingtoward;
    private float dotProd;

    public Ai_Patrol(StatePattern state)
    {
        unitstate = state;
    }
    public void UpdateState() 
    {

       // Debug.Log(" in the patrol state");
        look();
        Patrol();
    }
    public void ToPatrolState() { }
    public void ToAlertState() { unitstate.currentState = unitstate.alertState; }
    public void ToPursueState() { }
    public void ToMeleeAttackState() { }
    public void ToRangeAttackState() { }

    void look()
    {
        //Check medium range.
        colliders = Physics.OverlapSphere(unitstate.transform.position, unitstate.sightRange / 3, unitstate.myEnemyLayers);

        if (colliders.Length > 0)
        {
            VisibilityCalculations(colliders[0].transform);

            if (dotProd > 0)
            {
                AlertStateActions(colliders[0].transform);
                return;
            }
        }

        //Check max range.
        colliders = Physics.OverlapSphere(unitstate.transform.position, unitstate.sightRange, unitstate.myEnemyLayers);

        foreach (Collider col in colliders)
        {
            RaycastHit hit;

            VisibilityCalculations(col.transform);

            if (Physics.Linecast(unitstate.head.position, lookAtPoint, out hit, unitstate.sightLayers))
            {
                foreach (string tags in unitstate.myEnemyTags)
                {
                    if (hit.transform.CompareTag(tags))
                    {
                        if (dotProd > 0)
                        {
                            AlertStateActions(col.transform);
                            return;
                        }
                    }
                }
            }
        }
    }

   void Patrol()
    {
        unitstate.meshRendererFlag.material.color = Color.green;
      // Debug.Log("patrol state should be walking");

        if (!unitstate.myNavMeshAgent.enabled)
        {
            return;
        }

        if (unitstate.waypoints != null && unitstate.waypoints.Length > 0)
        {
            MoveTo(unitstate.waypoints[nextPoint].position);
            
            if (HaveIReachedDestination() && unitstate.waypoints.Length > 1)
            {
                nextPoint = (nextPoint + 1) % unitstate.waypoints.Length;
                return;
            }else
            {
                if (HaveIReachedDestination())
                {
                    
                    unitstate.waypoints = null;
                    //Debug.Log("waypoint length :" + unitstate.waypoints.Length);
                }
            }
        }
        else //Wander about if there are no waypoints
        {
           
            if (HaveIReachedDestination())
            {
                
                StopWalking();
                if (RandomWanderTarget(unitstate.transform.position, unitstate.sightRange, out unitstate.wanderTarget))
                {
                    //Debug.Log("Need to move randomly "+ unitstate.wanderTarget);
                    MoveTo(unitstate.wanderTarget);
                }
            }
        }


    }

    void AlertStateActions(Transform target)
    {
        unitstate.locationOfInterest = target.position; 
        //For check state
        ToAlertState();
    }

    void VisibilityCalculations(Transform target)
    {
        lookAtPoint = new Vector3(target.position.x, target.position.y + unitstate.offset, target.position.z);
        movingtoward = lookAtPoint - unitstate.transform.position;
        dotProd = Vector3.Dot(movingtoward, unitstate.transform.forward);
    }

    bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    {
        NavMeshHit navHit;

        Vector3 randomPoint = centre + Random.insideUnitSphere * unitstate.sightRange;
        if (NavMesh.SamplePosition(randomPoint, out navHit, 3.0f, NavMesh.AllAreas))
        {
            result = navHit.position; 
          //  Debug.Log("true Result " + result);
            return true;
        }
        else
        {
            result = centre;
        //    Debug.Log("false Result "+ result);
            return false;
        }
    }

    bool HaveIReachedDestination()
    {
        if (unitstate.myNavMeshAgent.remainingDistance <= unitstate.myNavMeshAgent.stoppingDistance &&
                    !unitstate.myNavMeshAgent.pathPending)
        {
          //  Debug.Log("idle because Re Dist is :" + unitstate.myNavMeshAgent.remainingDistance + "Stopping dist :"+ unitstate.myNavMeshAgent.stoppingDistance );
            StopWalking();
            return true;
        }

        else
        {
          //  Debug.Log("walking " + unitstate.myNavMeshAgent.remainingDistance);
            KeepWalking();
            return false;
        }
    }

    void MoveTo(Vector3 targetPos)
    {
        if (Vector3.Distance(unitstate.transform.position, targetPos) > unitstate.myNavMeshAgent.stoppingDistance + 1)
        {
            unitstate.myNavMeshAgent.SetDestination(targetPos);
            KeepWalking();
        }
    }

    void KeepWalking()
    {
       
        unitstate.myNavMeshAgent.isStopped = false;
        unitstate.AiMaster.CallEventUnitWalkAnim();
    }

    void StopWalking()
    {
       // Debug.Log("idle");
        unitstate.myNavMeshAgent.isStopped = true;
        unitstate.AiMaster.CallEventUnitIdleAnim();
    }

}
