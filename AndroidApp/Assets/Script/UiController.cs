using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [Header("Levels to Load")]
    public string startLevel;
    public Button[] lvlBtns;

    private int menuNum;

   
   

    [Header("Menu Values")]
    [SerializeField] private float defaultBrightness;
    [SerializeField] private float defaultVolume;
    [SerializeField] private float Volume;
    [SerializeField] private float Brightness;

    [Header("Menu Sliders")]
    [SerializeField] private Text BrightText;
    [SerializeField] private Slider BrightSlider;
    [Space(10)]
    [SerializeField] private Text volumeText;
    [SerializeField] private Slider VolumeSlider;

    [Header("Menu Dialogs")]
    [SerializeField] private GameObject noLevelDialog;
    [SerializeField] private GameObject loadLevelDialog;
    [SerializeField] private GameObject playLevelDialog;
    [Space(10)]
    [Header("Menu Comp")]
    [SerializeField] private GameObject LevelSelectionCanvas;
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject SettingCanvas;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject brightnessMenu;
    [SerializeField] private GameObject InGameStore;
    [SerializeField] private GameObject confirmationMenu;


    public void StartGame()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        menuNum = 1;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }
   public void OnMouseDown(string buttonType)
    {
        switch (buttonType)
        {
            case "sound":
                Debug.Log("Click");
                SettingCanvas.SetActive(false);
                brightnessMenu.SetActive(false);
                soundMenu.SetActive(true);
                menuNum = 4;
                break;
            case "brightness":
                SettingCanvas.SetActive(false);
                soundMenu.SetActive(false);
                brightnessMenu.SetActive(true);
                menuNum = 5;
                break;
            case "setting":
                GoBackToSettingsMenu();
                menuNum = 2;
                break;
            case "store":
                MenuCanvas.SetActive(false);
                SettingCanvas.SetActive(true);
                menuNum = 2;
                break;
            case "level":
                GoToLevelSelect();
                menuNum = 3;
                break;
            case "StartGame":
                MenuCanvas.SetActive(false);
                playLevelDialog.SetActive(true);
                menuNum = 7;
                break;
            case "Exit":
                Debug.Log("Quit!!!!");
                Application.Quit();
                break;
        }

    }

    public IEnumerator ConfirmationBox()
    {
        confirmationMenu.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationMenu.SetActive(false);
    }

    public void volumeSlider(float volume)
    {
        AudioListener.volume = volume;
        volumeText.text = volume.ToString("0.0");
    }

    public void SetVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        Debug.Log(PlayerPrefs.GetFloat("masterVolume"));
        StartCoroutine(ConfirmationBox());
    }
    public void resetVolume()
    {
        AudioListener.volume = defaultVolume;
        VolumeSlider.value = defaultVolume;
        volumeText.text = defaultVolume.ToString("0.0");
        SetVolume();
    }
    public void LevelSelection()
    {
        int LevelAt = PlayerPrefs.GetInt("levelAt", 0);
        for (int i = 0; i< lvlBtns.Length; i++)
        {
            if(i +2 > LevelAt)
            {
                lvlBtns[i].interactable = false;
            }
        }
    }

    public void GoBackToSettingsMenu()
    {
        MenuCanvas.SetActive(false);
        SettingCanvas.SetActive(true);
        soundMenu.SetActive(false);
        SetVolume();
        menuNum = 2;
    }
    public void GoBackToMainMenu()
    {
        MenuCanvas.SetActive(true);
       SettingCanvas.SetActive(false);
        brightnessMenu.SetActive(false);
        soundMenu.SetActive(false);
        menuNum = 1;
    }
    public void GoToLevelSelect()
    {
        MenuCanvas.SetActive(false);
        LevelSelectionCanvas.SetActive(true);
    }

}
