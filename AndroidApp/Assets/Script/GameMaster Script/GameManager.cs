using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private ClassList classlist;
   
    private double gold;
    private double gem;

    [SerializeField]
    private Text Goldtext;

    [SerializeField]
    private Text Gemtext;

    //public InventoryObject Inventory;
    //Inventory 
    [SerializeField]
    private Ui_Inventory ui_invent;
    public Inventory newInventory;
    

    public GameObject barrackUI;
    public GameObject ShopUI;
    public buttons Clickedbutton { get; private set; }

    private bool uiopen = false;
    public BuyBtn buybtn { get; private set; }
    public double Gold
    {
        get
        { return gold; }

        set
        {
            gold = value;
            Goldtext.text = value.ToString() + "<color=lime>£</color>";
        }
    }

    public double Gem
    {
        get
        { 
            return gem; 
        }
        set
        {
            gem = value;
            Gemtext.text = value.ToString() + "<color=lime>G</color>";
        }
    }
    void Start()
    {
        
        Gold = 2000;
        Gem = 0;
        newInventory = new Inventory();
        ui_invent.SetInventory(newInventory);
    }

  public void BuyUnits(BuyBtn button)
    {
       if(Gold >= button.Price )
        {
            this.buybtn = button;
            Gold -= button.Price;
            ShopUI.SetActive(false);
            ButtonsOnclicked();
          
        }
    }
    //Buying unit 
    public void ButtonsOnclicked()
    {
        ClassShuffle.onButtonClicked();
    }
    // when we select a unit  from the Barracks to place onto the map
    public void Selecton(buttons Buttons)
    {
        barrackUI.SetActive(false);
        Clickedbutton = Buttons;
        //  Hoverscript.Instance.Hoveractivation(Buttons.Getsprite());
    }
    // once we place the unit onto the map
    public void PlaceUnit()
    {
        Clickedbutton = null;
        //Hoverscript.Instance.Hoveractivation(null);
    }

    public void Togglebarrack()
    {
       
        if (uiopen == true)
        {
           barrackUI.SetActive(false);
           uiopen = false;
        }
        else { 
            barrackUI.SetActive(true); 
            uiopen = true;
            //ui_invent.PopulateInventory();
        }
        
    }

    public void ToggleShop()
    {
        if (uiopen == true)
        {
            ShopUI.SetActive(false);
            uiopen = false;
        }
        else
        {
            ShopUI.SetActive(true);
            uiopen = true;
        }
    }
}
