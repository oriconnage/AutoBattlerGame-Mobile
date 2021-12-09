using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShopUi : MonoBehaviour
{
    private GmRef GetRef;
    private GameManager_Master gm;
    public GameObject shopUi;
    public BuyBtn buybtn { get; private set; }
    

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
        if (shopUi != null)
        {
            shopUi.SetActive(!shopUi.activeSelf);
            gm.isShopUiOn = !gm.isShopUiOn;
            gm.CallEventShopUIToggle();
        }
        else
        {
            Debug.Log("Menu UI is null");
        }
    }

    public void BuyUnits(BuyBtn button)
    {
        if (GmRef.Instance.Gold >= button.Price)
        {
            this.buybtn = button;
            GmRef.Instance.Gold -= button.Price;
            shopUi.SetActive(false);
            ButtonsOnclicked();
        }
    }
    //Buying unit 
    public void ButtonsOnclicked()
    {
        ClassShuffle.onButtonClicked();
    }

}
