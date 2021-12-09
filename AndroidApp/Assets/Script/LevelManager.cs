using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager :Singleton<LevelManager>
{
    public Button lvlBtns;
    public bool isLocked;
    public Image unlockedImage;

    public void Update()
    {
        UpdateLevelImage();
    }
    public void UpdateLevelImage()
    {
        if (!isLocked)
        {
            unlockedImage.gameObject.SetActive(true);
            lvlBtns.interactable = false;
            
        }
        else
        {
            unlockedImage.gameObject.SetActive(false);
            lvlBtns.interactable = true;
        }
    }
}
