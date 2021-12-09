using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Object", menuName = "Inventory system/Units/Melee")]
public class MeleeClass : ClassObject
{
    [Range(20,40)]
    public int damage;

    [Range(90,200)]
    public float health;
    public void Awake()
    {
        
        type = CardType.Melee;
    }
}
