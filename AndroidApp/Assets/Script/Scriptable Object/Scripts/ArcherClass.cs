using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Archer Object", menuName = "Inventory system/Units/Archer")]
public class ArcherClass : ClassObject
{

    public void Awake()
    {

        type = CardType.Archer;
    }
}
