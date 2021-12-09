using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Inventory : Singleton<Ui_Inventory>
{
    private Inventory inventory;
    [SerializeField]
    private Transform UnitSlotContainer;
    [SerializeField]
    private GameObject SlotPrefab;
    private ToggleBarrackUi Gamemanager;
    public void SetInventory(Inventory tmp)
    {
        Debug.Log("SetInventory");
        inventory = tmp;
        
        inventory.UpdateList += onUpdated;
        RefeshInventoryItems();
    }

    private void onUpdated(object sender, System.EventArgs e)
    {
        RefeshInventoryItems();
    }
    public void RefeshInventoryItems()
    {
        foreach(Transform child in UnitSlotContainer)
        {
            if (child == UnitSlotContainer) continue;
            Destroy(child.gameObject);
        }
        foreach (ClassObject units in inventory.GetUnitList)
        {
            //instantiate the slot
            GameObject UnitSlot = Instantiate(SlotPrefab, UnitSlotContainer);
            // UnitSlot.AddComponent<buttons>(); // Added Button script
            buttons btn = new buttons();
            btn = UnitSlot.GetComponent<buttons>();
            Debug.Log("Ref " + units.name);
            //displaying the unit 

            Gamemanager = FindObjectOfType<ToggleBarrackUi>();
            btn.Unitprefab = units.prefab;
            if(btn.Unitprefab != null)
            {
                Debug.Log("prefab set");
            }
            btn.unitSprite = units.sprite;
            if (btn.unitSprite != null)
            {
                Debug.Log("Sprite set");
            }
            btn.SetImage();
            Button but = UnitSlot.GetComponent<Button>();
            but.onClick.AddListener(()=>Gamemanager.Selection(btn));
            
        }
       
    }
}
