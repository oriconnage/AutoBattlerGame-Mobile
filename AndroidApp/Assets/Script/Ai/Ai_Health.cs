using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Health : MonoBehaviour
{
    private Ai_Delegation AiMaster;
    [Range(100,200)]
    public int Health = 100;

    private void OnEnable()
    {
        AiMaster = GetComponent<Ai_Delegation>();
        AiMaster.EventDeductHealth += DeductHealth;
    }
    private void OnDisable()
    {
        AiMaster.EventDeductHealth -= DeductHealth;

    }

    void DeductHealth(int damage)
    {

        Health -= damage;
        if(Health <= 0)
        {
            Health = 0;
            AiMaster.CallEventUnitDeath();
            if (IsTagEnemy())
            {
                GmObjectiveManager.Instance.killobj.Increase();
            }
            else
            {
                GmObjectiveManager.Instance.gamelost.Deduct();
            }
            Destroy(gameObject, Random.Range(10, 20));
        }
    }
     bool IsTagEnemy(){
        if (gameObject.CompareTag("Enemy"))
        {
            return true;
        }
        else if (gameObject.CompareTag("Friendly"))
        {
            return false;
        }
        else
        {
            return false;
        }
    }
}
