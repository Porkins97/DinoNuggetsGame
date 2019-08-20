using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button yourButton;

	void Start () 
    {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
    {
		Debug.Log ("You have clicked the button!");
        ChangeScene();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

   public void ChangeScene()
    {
        SceneManager.LoadScene("DinoNuggetsPrototypeScene2");
        Debug.Log("Changed Scene");
    }
}
