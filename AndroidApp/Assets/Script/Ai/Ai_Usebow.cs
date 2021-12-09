using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Usebow : MonoBehaviour
{
    private Ai_RangeWeponManager WeaponMaster;
    private Transform myTransform;
    private RaycastHit hit;
    public LayerMask layersToDamage;
    public float range = 10;
   // private Ai_Delegation AIMaster;
    private StatePattern AiStatePattern;

    void OnEnable()
    {
        SetInitialReferences();
        WeaponMaster.EventAiInput += AiFireWeapon;
        ApplyLayersToDamage();
    }

    void OnDisable()
    {
        WeaponMaster.EventAiInput -= AiFireWeapon;
    }

    void SetInitialReferences()
    {
        WeaponMaster = GetComponent<Ai_RangeWeponManager>();
        myTransform = transform;

        if (transform.root.GetComponent<Ai_Delegation>() != null)
        {
            //AIMaster = transform.root.GetComponent<Ai_Delegation>();
            Debug.Log("Ai manager found");
        }

        if (transform.root.GetComponent<StatePattern>() != null)
        {
            AiStatePattern = transform.root.GetComponent<StatePattern>();
        }

    }
    void AiFireWeapon(float randomness)
    {
        //Debug.Log("Ai fire weapon");
        Vector3 startPosition = new Vector3(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness), 0.1f);
      //  Debug.Log("Ai fire weapon: "+ startPosition);
        if (Physics.Raycast(myTransform.TransformPoint(startPosition), myTransform.forward, out hit,range, layersToDamage))
        {
            if (hit.transform.GetComponent<Ai_TakeDamage>() != null)
            {
                // Debug.Log("Hit enemy");
                WeaponMaster.CallEventShotEnemy(hit, hit.transform);
            }

            else
            {
                // Debug.Log("Hit something");
                WeaponMaster.CallEventShotDefault(hit, hit.transform);
            }
        }
    }

    void ApplyLayersToDamage()
    {
        Invoke("ObtainLayersToDamage", 0.3f);
    }

    void ObtainLayersToDamage()
    {
        if (AiStatePattern != null)
        {
            layersToDamage = AiStatePattern.myEnemyLayers;
        }
    }


}

