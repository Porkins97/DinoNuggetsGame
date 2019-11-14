using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class ApplyHat : MonoBehaviour
{
    public int hatNum;
    public HatType hat;
    public GameObject[] hats;
    public bool randomHat;
    Transform hatLocator;
    GameObject appliedHat;
    public GameObject otherPlayer;
    public string customisationDataJSON = "customisationdata.json";
    public CustomisationData cd;
    public SaveData saveData;
    public int thisPlayer;
    public int p1, p2;
    private void Start()
    {
        hatLocator = this.gameObject.transform;
        if(randomHat != true)
        {
            int s = LoadData();
            PassHatnum(s);
        }
        else
        {
            PassHatnum(0);
        }
    }
    public void SelectHat()
    {
        if (randomHat == true)
        {
            hatNum = Random.Range(0, hats.Length);
            hat = (HatType)hatNum;
        }       
        for(int i = 0; i< hats.Length; i++)
        {
            if (hats[i].GetComponent<DinoCustomise>().hatType == hat)
            {
                appliedHat = Instantiate(hats[i], hatLocator.gameObject.transform);
                appliedHat.transform.parent = hatLocator.gameObject.transform;
                
                if(thisPlayer ==1)
                {
                    p1 = i;
                    cd.whichHatPlayerOne = i; 
                    cd.whichHatPlayerTwo = otherPlayer.GetComponent<ApplyHat>().p2;
                }
                else
                {
                    p2 = i;
                    cd.whichHatPlayerTwo = i; 
                    cd.whichHatPlayerOne = otherPlayer.GetComponent<ApplyHat>().p1;
                }
                saveData.GetNum(thisPlayer, i);
            }
        }
    }
    public void PassHatnum(int num)
    {
        hatNum = num;
        if (appliedHat != null)
            Destroy(appliedHat);
        hat = (HatType)hatNum;
        SelectHat();
    }
    public void PassNewHat(HatType _hat)
    {
        /*if (appliedHat != null)
            Destroy(appliedHat);
        hat = _hat;
        SelectHat();*/
    }

    public int LoadData()
    {
        string filePath = Application.streamingAssetsPath + "/" + customisationDataJSON;
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            cd = JsonUtility.FromJson<CustomisationData>(dataAsJson);
            p1 = cd.whichHatPlayerOne;
            p2 = cd.whichHatPlayerTwo;
            if(thisPlayer == 1)
            {
                int l = p1;
                return(l);
            }
            else
            {
                int l =p2;
                return(l);
            }
        }
        else
        {
            Debug.Log("Data Doesn't Exist");
            return(0);
        }
    }
}
