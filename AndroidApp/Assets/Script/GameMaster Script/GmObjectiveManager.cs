using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmObjectiveManager :Singleton<GmObjectiveManager>
{
    public GameManager_Master gm;
    private Objective updateobj;
    private Objective updateobj1;
    public KillCount killobj;
    public GameLost gamelost;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager_Master>();
        killobj = new KillCount(this);
        gamelost = new GameLost(this);
        updateobj = killobj;
        updateobj1 = gamelost;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { killobj.Increase(); }
        
        oncomplete();
    }
    public void oncomplete()
    {
        updateobj1.UpdateObj();
        updateobj.UpdateObj();
    }
}
