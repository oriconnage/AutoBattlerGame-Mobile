using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory system/Inventory")]

public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public void AddUnit(ClassObject _unit, int _amount)
    {
        bool hasItem = false;
        int i = 0;
        foreach ( var units in Container)
        {
            // check if we have a unit in the inventory 
            // if we do we can stack the unit
            if(Container[i].unit == _unit)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_unit, _amount));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ClassObject unit;
    public int amount;
    public InventorySlot(ClassObject _unit, int _amount)
    {
       unit = _unit;
        amount = _amount;
    }
    public void AddAmount(int val)
    {
        amount += val;
    }

}