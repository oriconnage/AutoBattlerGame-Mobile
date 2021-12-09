using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tank Object", menuName = "Inventory system/Units/Tank")]

public class TankClass : ClassObject
{
    [Range(20, 50)]
    public float Damage;
    [Range(200, 300)]
    public float health;
    public void Awake()
    {
        
        type = CardType.Tank;
    }
}
