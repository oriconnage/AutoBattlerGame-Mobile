using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBarrackUi : Singleton<ToggleBarrackUi>
{
    private GameManager_Master gm;
    public GameObject BarrackUi;

    public buttons Clickedbutton { get; private set; }
    private void OnEnable()
    {
        gm = GetComponent<GameManager_Master>();
        gm.MenuToggleEvent += toggleMenu;
    }
    private void OnDisable()
    {
        gm.MenuToggleEvent -= toggleMenu;
    }
    public void toggleMenu()
    {
        if (BarrackUi != null)
        {
            BarrackUi.SetActive(!BarrackUi.activeSelf);
            gm.isInventoryUIOn = !gm.isInventoryUIOn;
            gm.CallEventInventoryUIToggle();
        }
        else
        {
            Debug.Log("Menu UI is null");
        }
    }

    //selecting the button
    public void Selection(buttons Buttons)
    {
        BarrackUi.SetActive(false);
        Clickedbutton = Buttons;
        Debug.Log("Selected the button");
        
    }

    // once we place the unit onto the map we make the clicked button null
    public void PlaceUnit()
    {
        Clickedbutton = null;
        
    }
}
