using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_TakeDamage : MonoBehaviour
{
    private Ai_Delegation AiMaster;
    public int damageMultiplier = 1;
    public bool shouldRemoveCollider;

    void OnEnable()
    {
        SetInitialReferences();
        AiMaster.EventUnitDie += RemoveThis;
    }

    void OnDisable()
    {
        AiMaster.EventUnitDie -= RemoveThis;
    }

    void SetInitialReferences()
    {
        AiMaster = transform.root.GetComponent<Ai_Delegation>();
    }

    public void ProcessDamage(int damage)
    {
        int damageToApply = damage * damageMultiplier;
        AiMaster.CallEventDeductHealth(damageToApply);
    }

    void RemoveThis()
    {
        if (shouldRemoveCollider)
        {
            if (GetComponent<Collider>() != null)
            {
                Destroy(GetComponent<Collider>());
            }

            if (GetComponent<Rigidbody>() != null)
            {
                Destroy(GetComponent<Rigidbody>());
            }
        }

        gameObject.layer = LayerMask.NameToLayer("Default"); //So AI doesn't keep detecting.
  

        Destroy(this);
    }

}

