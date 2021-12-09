using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_RangeWeponManager : MonoBehaviour
{
    public delegate void RangeWeaponEventHandler(RaycastHit hitPosition, Transform hitTransform);
    public event RangeWeaponEventHandler EventShotDefault;
    public event RangeWeaponEventHandler EventShotEnemy;

    public delegate void AiGunHandler(float rnd);
    public event AiGunHandler EventAiInput;

    public bool isWeaponLoaded;
    public bool isReloading;

    public void CallEventShotDefault(RaycastHit hPos, Transform hTransform)
    {
        if (EventShotDefault != null)
        {
            EventShotDefault(hPos, hTransform);
        }
    }

    public void CallEventShotEnemy(RaycastHit hPos, Transform hTransform)
    {
        if (EventShotEnemy != null)
        {
            EventShotEnemy(hPos, hTransform);
        }
    }

    public void CallEventAiInput(float rand) 
    {
        if (EventAiInput != null)
        {
            EventAiInput(rand);
        }
    }
}
