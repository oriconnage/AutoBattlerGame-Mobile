using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassShuffle : Singleton<ClassShuffle>
{
    private GmRef gm;
    public static List<ClassObject> Classlist = new List<ClassObject>();
    public ClassShuffle() { Classlist = new List<ClassObject>(); }    // Constructor

    public static void onButtonClicked()
    {
        //Generate Deck of units 
        Generate();
        // Shuffle the deck
        Shuffle();
        //Draw the Card to the inventory 
        DrawCard();
    }


    public static void onButtonClickedtwo()
    {
        //Generate Deck of units 
        Generate();
        // Shuffle the deck
        Shuffle();
        //Draw the Card to the inventory 
        DrawSixCard();
    }
    // Create a deck of unit.
    public static void Generate()
    {

        foreach (MeleeClass melee in ClassList.Instance.meleeclass)
        {
            Debug.Log(melee.name);
            Classlist.Add(melee);
        }
        foreach (MageClass mage in ClassList.Instance.mageclass)
        {
            Debug.Log(mage.name);
            Classlist.Add(mage);
        }
        foreach (TankClass tank in ClassList.Instance.tankclass)
        {
            Debug.Log(tank.name);
            Classlist.Add(tank);
        }
        foreach(ArcherClass archer in ClassList.Instance.archerclass)
        {
            Debug.Log(archer.name);
            Classlist.Add(archer);
        }
    }
    public static void Shuffle()
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < Classlist.Count; i++)
        {
            //Random for Remaining pos
            int r = rnd.Next(i, Classlist.Count);

            //swapping the elements    
            var tmp = Classlist[i];
            Classlist[i] = Classlist[r];
            Classlist[r] = tmp;
            
        }
        //  DrawCard(deck);
    }
    public static void DrawCard()
    {

        Inventory inventory = GmRef.Instance.newInventory;
        for (int i = 0; i != 3; i++)
        {
            inventory.AddUnits(Classlist[i]);
        }
    }

    public static void DrawSixCard()
    {
        Inventory inventory = GmRef.Instance.newInventory;
        for (int i = 0; i != 6; i++)
        {
            inventory.AddUnits(Classlist[i]);
        }
    }
}
