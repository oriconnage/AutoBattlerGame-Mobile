using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ai_Interface
{    
        void UpdateState();
        void ToPatrolState();
        void ToAlertState();
        void ToPursueState();
        void ToMeleeAttackState();
        void ToRangeAttackState();
}
