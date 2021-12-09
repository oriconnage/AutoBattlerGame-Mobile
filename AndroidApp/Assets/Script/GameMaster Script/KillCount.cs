using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCount : Objective
{
    private readonly GmObjectiveManager obj;
    private LevelManager nextlevel;
   [SerializeField]  
    private string disc;
    private int count = 0;
    private int targetcount;

    public KillCount(GmObjectiveManager i)
    {
        obj = i;
    }
    public string getDescription() {
        return disc; 
    }
    public void Increase()
    {
        GmRef.Instance.enemyKilled++;
        count++;
       // Debug.Log("kill count :" + count);
    }
    public void Deduct() { }
    public void UpdateObj()
    {
        
        if (isCompleted())
        {
          //  Debug.Log("Levelcompleted");
            //unlock next level 
           obj.gm.CallEventGameOver();
            //player get sent to main menu 
           obj.gm.isGameOver = true;
        }

    }
    
    public bool isCompleted()
    {
        return (GmRef.Instance.enemyKilled >= EnemySpawner.Instance.numberToSpawn);
    }
    
}
