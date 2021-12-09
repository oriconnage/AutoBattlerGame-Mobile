using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Archer Object", menuName = "Inventory system/Units/Archer")]
public class ArcherClass : ClassObject
{
    
    [Range(15,20)]
    public float Range;

    [Range(20, 30)]
    public float Damage;
    public void Awake()
    {
        Id = 0;
        type = CardType.Archer;
    }
}
