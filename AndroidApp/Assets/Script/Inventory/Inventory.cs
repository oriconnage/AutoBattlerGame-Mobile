using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler UpdateList;
    private List<ClassObject> UnitListContainer;

    public Inventory()
    {
        UnitListContainer = new List<ClassObject>();
        //AddUnits(new Unit { type = Unit.Type.Mage });
        Debug.Log("Create a Inventory");
    }
    public void AddUnits (ClassObject unit)
    {
        UnitListContainer.Add(unit);
        UpdateList?.Invoke(this, EventArgs.Empty);
      //  Debug.Log("Adding "+ UnitListContainer.Count); 
    }
    public List<ClassObject> GetUnitList
    {
        get
        { 
            return UnitListContainer;
        }
    }

}