using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Delegation : MonoBehaviour
{
    public delegate void AiHandler();
    public event AiHandler EventUnitDie;
    public event AiHandler EventUnitWalkAnim;
    public event AiHandler EventUnitAttackAnim;
    public event AiHandler EventUnitIdleAnim;
    public event AiHandler EventUnitRecoveredAnim;
    public event AiHandler EventUnitStruckAnim;

    public delegate void HealthHandler(int health);
    public event HealthHandler EventDeductHealth;

    //Used for animation
    public string animationBoolPursuing = "isPursuing";
    public string animationTriggerStruck = "Struck";
    public string animationTriggerMelee = "Attack";
    public string animationTriggerRecovered = "Recovered";

    public void CallEventUnitStuck()
    {
        if(EventUnitStruckAnim != null)
        {
            EventUnitStruckAnim();
        }
    }
    public void CallEventUnitDeath()
    {
        if(EventUnitDie != null)
        {
            EventUnitDie();
        }
    }
  
    public void CallEventUnitWalkAnim()
    {
        if (EventUnitWalkAnim != null)
        {
            EventUnitWalkAnim();
        }
    }
    public void CallEventUnitAttackAnim()
    {
        if (EventUnitAttackAnim != null)
        {
            EventUnitAttackAnim();
        }
    }
    public void CallEventUnitIdleAnim()
    {
        if (EventUnitIdleAnim != null)
        {
            EventUnitIdleAnim();
        } 
    }
    public void CallEventUnitRecoveredAnim()
        {
            if (EventUnitRecoveredAnim != null)
            {
                EventUnitRecoveredAnim();
            }
        }
    public void CallEventDeductHealth(int health)
    {
        if (EventDeductHealth != null)
        {
            EventDeductHealth(health);
        }
    }


}
