using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Serializable]
    public class PlayerStuff
    {
        public GameObject PlayerName;
        public GameObject playersPan;
        public GameObject playersPot;
        public GameObject playersCuttingBoard;
        public int playerScore;
    }

    [SerializeField] public PlayerStuff playerStuff;
    [SerializeField] Text score;

    // Start is called before the first frame update
    void Start()
    {
        playerStuff = new PlayerStuff();
        playerStuff.playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = String.Format("Score: {0}", playerStuff.playerScore);
    }
    public void UpdateScore()
    {
        playerStuff.playerScore++;
    }
}
