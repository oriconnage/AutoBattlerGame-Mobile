using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSetAtttacker : MonoBehaviour
{
    private StatePattern npcStatePattern;

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        npcStatePattern = GetComponent<StatePattern>();
    }

    public void SetMyAttacker(Transform attacker)
    {
        npcStatePattern.myAttacker = attacker;  
    }
}

