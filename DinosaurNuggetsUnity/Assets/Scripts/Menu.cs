using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text HighScoreText;

    void Start () 
    {
        HighScoreText.text = "" + (int)PlayerPrefs.GetFloat("HighScore");
    }
    public void changescene(string Game)
    {
        Application.LoadLevel (Game);
    }
}