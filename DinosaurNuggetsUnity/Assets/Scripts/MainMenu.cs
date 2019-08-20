using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button LevelSelectButton;
    public Button ControlsButton;
    public Button HomeLevel;
    public Button HomeControls;

    string Kitchen = "DinoNuggetsPrototypeScene2";

    public GameObject Homescreen;
    public GameObject LevelSelectScreen;
    public GameObject ControlsScreen;

    bool MainMenuActive = true;
    bool LevelSelectActive = false;
    bool ControlsActive = false;

	void Start () 
    {
		Button btnPlay = PlayButton.GetComponent<Button>();
		btnPlay.onClick.AddListener(TaskOnClickPlay);

        Button btnSelect = LevelSelectButton.GetComponent<Button>();
        btnSelect.onClick.AddListener(TaskOnClickLevel);

        Button btnControls = ControlsButton.GetComponent<Button>();
        btnControls.onClick.AddListener(TaskOnClickControls);

        Button btnHomeLevel = HomeLevel.GetComponent<Button>();
        btnHomeLevel.onClick.AddListener(TaskOnClickHome);

        Button btnHomeControls = HomeControls.GetComponent<Button>();
        btnHomeControls.onClick.AddListener(TaskOnClickHome);

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
        ControlsActive = false;
        UpdateUI();
    }
    void TaskOnClickControls()
    {
        Debug.Log("You have clicked the Controls button!");
        MainMenuActive = false;
        LevelSelectActive = false;
        ControlsActive = true;
        UpdateUI();
    }
    void TaskOnClickHome()
    {
        Debug.Log("You have clicked the Home button!");
        MainMenuActive = true;
        LevelSelectActive = false;
        ControlsActive = false;
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

        if(ControlsActive == true)
        {
            ControlsScreen.SetActive(true);
        }
        else
        {
            ControlsScreen.SetActive(false);
        }
    }
}
