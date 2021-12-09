using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
  

 [System.Serializable]
    public struct Unit
    {
        // Is the Prefab index 
        public enum Type { Archer= 0, Melee = 1, Tank = 2, Mage = 3, Knight =4 };
        public Type type; // for defining the Type of units
    }


    [System.Serializable]
    public class ShuffleUnit: Singleton<ShuffleUnit>
    {
        public List<Unit> unitList; // List units from ClassObject script

       
        public ShuffleUnit() { unitList = new List<Unit>();}// Constructor

        //Generate 5 of each units type 
        public void Generate()
        {
            Unit newUnit = new Unit();
            
            for(int i =  0; i !=5; i++)
            {
                newUnit.type = (Unit.Type)i; // loops through the unit types
                for( int g = 0;g < 5; g++)
                {
                    this.unitList.Add(newUnit);
                  //  Debug.Log(unit[i]);
                }
            }
        }
        public static void  Shuffle(ShuffleUnit deck)
        {
            System.Random rnd = new System.Random();
            for(int i = 0; i < deck.unitList.Count; i++)
            {   
                //Random for Remaining pos
                int r = rnd.Next(i, deck.unitList.Count);
                //swapping the elements    
                Unit tmp = deck.unitList[i];
                deck.unitList[i] = deck.unitList[r];
                deck.unitList[r] = tmp;
               // Debug.Log(deck.unitList[i].type);
            }
          //  DrawCard(deck);
        }
    public static void DrawCard(ShuffleUnit deck)
    {
        string t;
        Inventory inventory = new Inventory();
        for (int i = 0; i != 3; i++)
        {
          //  t = (deck.unitList[i].type.ToString());
           // inventory.AddUnits(new ClassObject { type = dek.unitList[i].type });
         
        }
        // BuyBtn NewCard = Instantiate(BuyBtn.Instance.Unitcard[]).GetComponent<BuyBtn>();
    }
  
    }


