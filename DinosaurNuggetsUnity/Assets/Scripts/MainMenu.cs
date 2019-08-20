using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button LevelSelectButton;
    public Button CreditsButton;
    public Button HomeLevel;
    public Button HomeCredits;

    string Kitchen = "DinoNuggetsPrototypeScene2";

    public GameObject Homescreen;
    public GameObject LevelSelectScreen;
    public GameObject CreditsScreen;

    bool MainMenuActive = true;
    bool LevelSelectActive = false;
    bool CreditsActive = false;

	void Start () 
    {
		Button btnPlay = PlayButton.GetComponent<Button>();
		btnPlay.onClick.AddListener(TaskOnClickPlay);

        Button btnSelect = LevelSelectButton.GetComponent<Button>();
        btnSelect.onClick.AddListener(TaskOnClickLevel);

        Button btnCredit = CreditsButton.GetComponent<Button>();
        btnCredit.onClick.AddListener(TaskOnClickCredit);

        Button btnHomeLevel = HomeLevel.GetComponent<Button>();
        btnHomeLevel.onClick.AddListener(TaskOnClickHome);

        Button btnHomeCredits = HomeCredits.GetComponent<Button>();
        btnHomeCredits.onClick.AddListener(TaskOnClickHome);

        UpdateUI();
    }

	void TaskOnClickPlay()
    {
		Debug.Log ("You have clicked the Play button!");
        ChangeScene(Kitchen);
	}
    void TaskOnClickLevel()
    {
        Debug.Log("You have clicked the Level Select button!");
        MainMenuActive = false;
        LevelSelectActive = true;
        CreditsActive = false;
        UpdateUI();
    }
    void TaskOnClickCredit()
    {
        Debug.Log("You have clicked the Credit button!");
        MainMenuActive = false;
        LevelSelectActive = false;
        CreditsActive = true;
        UpdateUI();
    }
    void TaskOnClickHome()
    {
        Debug.Log("You have clicked the Home button!");
        MainMenuActive = true;
        LevelSelectActive = false;
        CreditsActive = false;
        UpdateUI();
    }

   public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("Changed Scene");
    }

    void UpdateUI()
    {
        if(MainMenuActive == true)
        {
            Homescreen.SetActive(true);
        }
        else
        {
            Homescreen.SetActive(false);
        }

        if(LevelSelectActive == true)
        {
            LevelSelectScreen.SetActive(true);
        }
        else
        {
            LevelSelectScreen.SetActive(false);
        }

        if(CreditsActive == true)
        {
            CreditsScreen.SetActive(true);
        }
        else
        {
            CreditsScreen.SetActive(false);
        }
    }
}
