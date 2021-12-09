using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    private GameManager_Master gm;
    public GameObject menu;


    private void OnEnable()
    {
      gm = GetComponent<GameManager_Master>();
      gm.GameOverEvent += toggleMenu;
    }
    private void OnDisable()
    {
        gm.GameOverEvent -= toggleMenu;
    }
    private void toggleMenu()
    {
        if (menu != null)
        {
            menu.SetActive(!menu.activeSelf);
            gm.isMenuOn = !gm.isMenuOn;
            gm.CallEventMenuToggle();
        }
        else
        {
            Debug.Log("Menu UI is null");
        }
    }


}
