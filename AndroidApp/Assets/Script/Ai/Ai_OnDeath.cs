using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_OnDeath : MonoBehaviour
{
        private Ai_Delegation AiMaster;
        private StatePattern AiPattern;
        private Animator myAnimator;
        private NavMeshAgent myNavMeshAgent;

        void OnEnable()
        {
            SetInitialReferences();
            AiMaster.EventUnitDie += TurnOffStatePattern;
            AiMaster.EventUnitDie += TurnOffNavMeshAgent;
            AiMaster.EventUnitDie += TurnOffAnimator;
        }

        void OnDisable()
        {
            AiMaster.EventUnitDie -= TurnOffStatePattern;
            AiMaster.EventUnitDie -= TurnOffNavMeshAgent;
            AiMaster.EventUnitDie -= TurnOffAnimator;
        }

        void SetInitialReferences()
        {
            AiMaster = GetComponent<Ai_Delegation>();

            if (GetComponent<NavMeshAgent>() != null)
            {
                myNavMeshAgent = GetComponent<NavMeshAgent>();
            }
            if (GetComponent<Animator>() != null)
            {
            myAnimator = GetComponent<Animator>();
            } 
        }

        void TurnOffNavMeshAgent()
        {
            if (myNavMeshAgent != null)
            {
                myNavMeshAgent.enabled = false;
            }
        }

    void TurnOffStatePattern()
    {
        if (AiPattern != null)
        {
            AiPattern.enabled = false;
        }
    }
    void TurnOffAnimator()
    {
        if (myAnimator != null)
        {
            myAnimator.enabled = false;
        }
    }
}
