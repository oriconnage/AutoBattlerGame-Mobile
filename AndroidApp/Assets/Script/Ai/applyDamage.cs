using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyDamage : MonoBehaviour
{
    private Ai_RangeWeponManager gunMaster;
    public int damage = 10;

    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventShotEnemy += ApplyDamage;
        gunMaster.EventShotDefault += ApplyDamage;
    }

    void OnDisable()
    {
        gunMaster.EventShotEnemy -= ApplyDamage;
        gunMaster.EventShotDefault -= ApplyDamage;
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<Ai_RangeWeponManager>();
    }

    void ApplyDamage(RaycastHit hitPosition, Transform hitTransform)
    {
        hitTransform.SendMessage("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
    //hitTransform.SendMessage("CallEventPlayerHealthDeduction", damage, SendMessageOptions.DontRequireReceiver);
        hitTransform.root.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
    }
}
