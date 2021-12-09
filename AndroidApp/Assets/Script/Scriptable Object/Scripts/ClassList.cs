using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassList : Singleton<ClassList>
{
   // public static List<ClassObject> Classlist = new List<ClassObject>();

    public MeleeClass[] meleeclass;
    public MageClass[] mageclass;
    public TankClass[] tankclass;
    public ArcherClass[] archerclass;
    public void Start()
    {
        
    }

}
