using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLost : Objective
{
    private readonly GmObjectiveManager obj;
    [SerializeField]
    private string disc;
    private int UnitonField;

    public GameLost(GmObjectiveManager i)
    {
        obj = i;
    }
    public string getDescription()
    {
        return disc;
    }
    public void Awake()
    {
        UnitonField = GmRef.Instance.unitOnField;
       
    }
    public void Increase() { }
    public void Deduct()
    {
        GmRef.Instance.unitOnField -= 1;
    }
    public void UpdateObj()
    {
        if (StartGame.Instance.gm.isReady)
        {
            if (isCompleted())
            {
                obj.gm.CallEventRestartLevel();
                obj.gm.isGameLost = true;
            }
        }

    }

    public bool isCompleted()
    {
        return (GmRef.Instance.unitOnField <= 0);
    }

}
