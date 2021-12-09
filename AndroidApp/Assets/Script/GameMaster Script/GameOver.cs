using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    [SerializeField]
    private GameObject GameLostPanel;
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private GameObject Rewardpanel;
    [SerializeField]
    private Text GoldReward;
    [SerializeField]
    private Text GemReward;
    [SerializeField]
    private GameObject ContinueBtn;
    [SerializeField]
    private GameObject RestartBtn;

    public string LevelStr;
    public SceneFader fader;

    public int unlocked;
    private void Start()
    {
        unlocked = SceneManager.GetActiveScene().buildIndex + 1;
    }
    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.GameOverEvent += TurnOnGameOverPanel;
        gameManagerMaster.RestartLevelEvent += TurnOnGameLostPanel;
    }

    void OnDisable()
    {
        gameManagerMaster.GameOverEvent -= TurnOnGameOverPanel;
        gameManagerMaster.RestartLevelEvent -= TurnOnGameLostPanel;
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    void TurnOnGameOverPanel()
    {
        if ( panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            //inventory.SetActive(false);
        }
    }
    public void ClickReward()
    {
        Rewardpanel.SetActive(true);
        GoldReward.text = " + " + (GmRef.Instance.Gold + 2500).ToString();
        GemReward.text = " + " + (GmRef.Instance.Gem + 15).ToString();
        ContinueBtn.SetActive(true);
    }
    public void continueToNextScene()
    {
        PlayerPrefs.SetInt("levelReached", unlocked);
        fader.FadeTo(LevelStr);
        
    }

    public void TurnOnGameLostPanel()
    {
        if (GameLostPanel != null)
        {
            GameLostPanel.SetActive(true);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
