using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelSelector : MonoBehaviour
{
    public SceneFader fader;
    public Button[] buttons;
    public bool isLocked;
   // public Image[] unlockedImage;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i+1 > levelReached)
            {

                buttons[i].GetComponent<LevelManager>().lvlBtns.interactable = false;
                buttons[i].GetComponent<LevelManager>().isLocked = false;
                //unlockedImage[i].gameObject.SetActive(true);
            }
            else {
                buttons[i].GetComponent<LevelManager>().isLocked = true;
                buttons[i].GetComponent<LevelManager>().lvlBtns.interactable = true;
            }
        }
    }

    public void OnMouseDown(string Name)
    {
      fader.FadeTo(Name);   
    }
}
