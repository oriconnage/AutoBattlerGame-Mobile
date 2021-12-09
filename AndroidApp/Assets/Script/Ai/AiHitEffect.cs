using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHitEffect : MonoBehaviour
{
    private Ai_RangeWeponManager gunMaster;
    public GameObject defaultHitEffect;
    public GameObject enemyHitEffect;

    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventShotDefault += SpawnDefaultHitEffect;
        gunMaster.EventShotEnemy += SpawnEnemyHitEffect;
    }

    void OnDisable()
    {
        gunMaster.EventShotDefault -= SpawnDefaultHitEffect;
        gunMaster.EventShotEnemy -= SpawnEnemyHitEffect;
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<Ai_RangeWeponManager>();
    }

    void SpawnDefaultHitEffect(RaycastHit hitPosition, Transform hitTransform)
    {
        if (defaultHitEffect != null)
        {
          //  Debug.Log("Hit something");
            Quaternion quatAngle = Quaternion.LookRotation(hitPosition.normal);
            Instantiate(defaultHitEffect, hitPosition.point, quatAngle);
        }
    }

    void SpawnEnemyHitEffect(RaycastHit hitPosition, Transform hitTransform)
    {
        if (enemyHitEffect != null)
        {
            //Debug.Log("Hit enemy");
            Quaternion quatAngle = Quaternion.LookRotation(hitPosition.normal);
            Instantiate(enemyHitEffect, hitPosition.point, quatAngle);
        }
    }
}
