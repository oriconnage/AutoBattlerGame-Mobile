using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
public class GmRef : Singleton<GmRef>
{
    [SerializeField]
    private ClassList classlist;

    private double gold;
    private double gem;
    private int EnemyKilled;
    [SerializeField]
    private Text KillText;
    private int number;
    [SerializeField]
    private Text Countdown;
    
    [SerializeField]
    private Text Goldtext;
    [SerializeField]
    private Text Gemtext;

    [SerializeField]
    private NavMeshSurface navMeshSurface;
    [SerializeField]
    private Text unitOnFieldText;
    private int unitonField;
    public int Availableunit;

    //Inventory 
    public Ui_Inventory ui_invent;
    public Inventory newInventory;
    public Transform [] Waypoint;
    public Transform UnitPos;

    public double Gold
    {
        get
        { return gold; }

        set
        {
            gold = value;
            Goldtext.text = value.ToString() + "<color=lime>£</color>";
        }
    }

    public double Gem
    {
        get
        {
            return gem;
        }
        set
        {
            gem = value;
            Gemtext.text = value.ToString() + "<color=lime>G</color>";
        }
    }

    public int countDown
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            Countdown.text = value.ToString();
        }
    }

    public int unitOnField
    {
        get
        {
            return unitonField;
        }
        set
        {
            unitonField = value;
            unitOnFieldText.text = value.ToString() + " / "+ Availableunit.ToString();
        }
    }

    public int enemyKilled
    {
        get
        {
            return EnemyKilled;
        }
        set
        {
            EnemyKilled = value;
            KillText.text = value.ToString() + " / " + EnemySpawner.Instance.numberToSpawn.ToString();
        }
    }

    private void Awake()
    {
        navMeshSurface.BuildNavMesh();
    }
    void Start()
    {
        Gold = 2000;
        Gem = 10;
        newInventory = new Inventory();
        ui_invent.SetInventory(newInventory);
        countDown = 10;
       unitOnField = 0;
        enemyKilled = 0;
    }
    public bool isGameWon()
    {
        return EnemyKilled == EnemySpawner.Instance.numberToSpawn;
    }
    public bool isGameLost()
    {
        return unitonField <= 0;
    }
}
