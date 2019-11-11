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
    public Transform hatLocator;
    GameObject appliedHat;
    public string customisationDataJSON = "customisationdata.json";
    public CustomisationData cd;

    private void Start()
    {
        if(randomHat != true)
        {
            int l = LoadData();
            PassHatnum(l);
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
                cd.whichHatPlayerOne = i;
                SavaData();
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
        if (appliedHat != null)
            Destroy(appliedHat);
        hat = _hat;
        SelectHat();
    }

    public void SavaData()
    {
        Debug.Log("Saving");
        string dataAsJson = JsonUtility.ToJson(cd);
        Debug.Log(dataAsJson);
        string filePath = Application.streamingAssetsPath + "/" + customisationDataJSON;
        File.WriteAllText(filePath, dataAsJson);
    }

    public int LoadData()
    {
        string filePath = Application.streamingAssetsPath + "/" + customisationDataJSON;
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            CustomisationData cData = JsonUtility.FromJson<CustomisationData>(dataAsJson);
            int l = cData.whichHatPlayerOne;
            //int i = cData.whichHatPlayerTwo; if this is player2
            return (l);
        }
        else
        {
            Debug.Log("Data Doesn't Exist");
            return(0);
        }
        
    }
}
