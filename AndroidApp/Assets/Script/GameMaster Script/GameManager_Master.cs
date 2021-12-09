using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Master : MonoBehaviour
{
    // Class contains all of the delegated events for the GameManager
    public delegate void GameManagerEvent();
    public event GameManagerEvent StartGameEvent;
    public event GameManagerEvent MenuToggleEvent;
    public event GameManagerEvent InventoryUIToggleEvent;
    public event GameManagerEvent ShopToggleEvent;
    public event GameManagerEvent RestartLevelEvent;
    public event GameManagerEvent GoToMenuSceneEvent;
    public event GameManagerEvent GameOverEvent;


    public bool isReady;
    public bool isGameOver;
    public bool isGameLost;
    public bool isInventoryUIOn;
    public bool isShopUiOn;
    public bool isMenuOn;
    public bool isCountdown;
 


    public void CallEventStartGame()
    {
        if (StartGameEvent != null)
        {
            StartGameEvent();
        }
    }
    public void CallEventMenuToggle()
    {
        if (MenuToggleEvent != null)
        {
            MenuToggleEvent();
        }
    }
    public void CallEventInventoryUIToggle()
    {
        if (InventoryUIToggleEvent != null)
        {
            InventoryUIToggleEvent();
        }
    }
    public void CallEventShopUIToggle()
    {
        if (ShopToggleEvent != null)
        {
           ShopToggleEvent();
        }
    }
    public void CallEventRestartLevel()
    {
        if (RestartLevelEvent != null)
        {
            if (!isGameLost)
            {
                isGameLost = true;
                RestartLevelEvent();
            }
        }
    }
    public void CallEventGoToMenuScene()
    {
        if (GoToMenuSceneEvent != null)
        {
            GoToMenuSceneEvent();
        }
    }
    public void CallEventGameOver()
    {
        if (GameOverEvent != null)
        {
            if (!isGameOver)
            {
                isGameOver = true;
                GameOverEvent();
            }
        }
    }
}
