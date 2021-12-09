using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mage Object", menuName = "Inventory system/Units/Mage")]

public class MageClass : ClassObject
{
    [Range(15, 20)]
    public float Range;
    [Range(20, 30)]
    public float Damage;
    [Range(100, 200)]
    public float health;
    public void Awake()
    {
        
        type = CardType.Mage;
    }
}
