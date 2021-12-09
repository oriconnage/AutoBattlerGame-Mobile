using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAmination : MonoBehaviour
{
    private Ai_Delegation AiMaster;
    private Animator myAnimator;

    void OnEnable()
    {
        SetInitialReferences();
        AiMaster.EventUnitAttackAnim += ActivateAttackAnimation;
        AiMaster.EventUnitWalkAnim += ActivateWalkingAnimation;
        AiMaster.EventUnitIdleAnim += ActivateIdleAnimation;
        AiMaster.EventUnitRecoveredAnim += ActivateRecoveredAnimation;
        AiMaster.EventUnitStruckAnim += ActivateStruckAnimation;
    }

    void OnDisable()
    {
        AiMaster.EventUnitAttackAnim -= ActivateAttackAnimation;
        AiMaster.EventUnitWalkAnim -= ActivateWalkingAnimation;
        AiMaster.EventUnitIdleAnim -= ActivateIdleAnimation;
        AiMaster.EventUnitRecoveredAnim -= ActivateRecoveredAnimation;
        AiMaster.EventUnitStruckAnim -= ActivateStruckAnimation;
    }

    void SetInitialReferences()
    {
        AiMaster = GetComponent<Ai_Delegation>();

        if (GetComponent<Animator>() != null)
        {
            myAnimator = GetComponent<Animator>();
        }
    }

    void ActivateWalkingAnimation()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
             //   Debug.Log("walkin anim");
                myAnimator.SetBool(AiMaster.animationBoolPursuing, true);
            }
        }
    }

    void ActivateIdleAnimation()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetBool(AiMaster.animationBoolPursuing, false);
            }
        }
    }

    void ActivateAttackAnimation()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetTrigger(AiMaster.animationTriggerMelee);
            }
        }
    }

    void ActivateRecoveredAnimation()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetTrigger(AiMaster.animationTriggerRecovered);
            }
        }
    }

    void ActivateStruckAnimation()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetTrigger(AiMaster.animationTriggerStruck);
            }
        }
    }


}
