using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePause : MonoBehaviour
{
    private GameManager_Master gm;
    private bool isPaused;

    private void OnEnable()
    {
        gm = GetComponent<GameManager_Master>();
        gm.MenuToggleEvent += PauseGame;
        gm.StartGameEvent += PauseGame;
    }
    private void OnDisable()
    {
        gm.StartGameEvent -= PauseGame;
        gm.MenuToggleEvent -= PauseGame;
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Debug.Log("Game Running");
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Debug.Log("is paused");
            Time.timeScale = 0;
            isPaused = true;
        }
    }


}
