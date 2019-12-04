using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text HighScoreText;
    public GameObject player1;
    public GameObject player2;

    void Awake()
    {
        if (GameManager.instance.playerWon == Players.Player1)
        {
            player1.SetActive(true);
            player2.SetActive(false);
        }
        else
        {
            player2.SetActive(true);
            player1.SetActive(false);
        }
        
    }

    void Start () 
    {
        HighScoreText.text = "" + (int)PlayerPrefs.GetFloat("HighScore");
    }
    public void changescene(string Game)
    {
        SceneManager.LoadScene(Game);
    }
}