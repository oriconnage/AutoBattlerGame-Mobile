using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : Singleton<StartGame>
{ 
    public GameManager_Master gm;
    public GameObject CountDownUi;
    public GameObject buttonPanelUi;
    

    private void Start()
    {
        CountDownUi.SetActive(false);
       
        
    }
    private void OnEnable()
    {
        gm = GetComponent<GameManager_Master>();
        gm.StartGameEvent += toggleMenu;
    }
    private void OnDisable()
    {
        gm.StartGameEvent -= toggleMenu;
    }
    public void toggleMenu()
    {
        if (CountDownUi != null)
        {
          //  CountDownUi.SetActive(!CountDownUi.activeSelf);
            gm.isCountdown = !gm.isCountdown;
            StartCoroutine("CountTimer");
        }
        else
        {
            Debug.Log("Menu UI is null");
        }
    }
    IEnumerator CountTimer()
    {
        //float pausedTime = Time.realtimeSinceStartup + 3;
    //    while (Time.realtimeSinceStartup < pausedTime)
      //  {
               yield return new WaitForSeconds(1.0f);
       // }
        Debug.Log("delay start");
        //pause.PauseGame();
        //CountDownUi.SetActive(false);
        buttonPanelUi.SetActive(false);
        gm.isReady = true;

    }
}
