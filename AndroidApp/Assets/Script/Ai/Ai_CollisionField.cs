using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_CollisionField : MonoBehaviour
{
    private Ai_Delegation AiMaster;
    private Rigidbody rigidBodyStrikingMe;
    private int damageToApply;
    public float massRequirement = 50;
    public float speedRequirement = 5;
    private float damageFactor = 0.1f;

    void OnEnable()
    {
        AiMaster = transform.root.GetComponent<Ai_Delegation>();
        AiMaster.EventUnitDie += DisableThis;
    }

    void OnDisable()
    {
        AiMaster.EventUnitDie -= DisableThis;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            rigidBodyStrikingMe = other.GetComponent<Rigidbody>();

            if (rigidBodyStrikingMe.mass >= massRequirement &&
                rigidBodyStrikingMe.velocity.sqrMagnitude >= speedRequirement * speedRequirement)
            {
                damageToApply = (int)(rigidBodyStrikingMe.mass * rigidBodyStrikingMe.velocity.magnitude * damageFactor);
                AiMaster.CallEventDeductHealth(damageToApply);
            }
        }
    }

    void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
