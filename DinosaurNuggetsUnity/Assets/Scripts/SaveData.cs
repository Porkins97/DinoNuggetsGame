using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class SaveData : MonoBehaviour
{
    public string customisationDataJSON = "customisationdata.json";
    public CustomisationData cd;
    public GameObject Player1;
    public GameObject Player2;
    public int playerOne, playerTwo;
    void Save()
    {
        Debug.Log("Saving");
        string dataAsJson = JsonUtility.ToJson(cd);
        Debug.Log(dataAsJson);
        string filePath = Application.streamingAssetsPath + "/" + customisationDataJSON;
        File.WriteAllText(filePath, dataAsJson);
    }

    public void GetNum(int playerNum, int hatNum)
    {
        if(playerNum == 1)
        {
            Debug.Log("PlayerOne");
            playerOne = hatNum;
            playerTwo = Player2.GetComponent<ApplyHat>().hatNum;
        }
        else
        {
            Debug.Log("PlayerTwo");
            playerTwo = hatNum;
            playerOne = Player1.GetComponent<ApplyHat>().hatNum;
        }
        cd.whichHatPlayerOne = playerOne;
        cd.whichHatPlayerTwo = playerTwo;
        Save();
    }
}
