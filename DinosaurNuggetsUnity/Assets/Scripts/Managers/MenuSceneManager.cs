using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    public SO_Hats soHats;
    public List<Hat> hats;
    private string hatPath = "Assets/Database/Other";
    public Transform HatMenuPanel;
    public GameObject dino;
    public GameObject ButtonPrefab;

    void Awake()
    {
        hats = new List<Hat>();
        foreach (string strPath in AssetDatabase.FindAssets("t:SO_Hats", new[] { hatPath }))
        {
            soHats = ((SO_Hats)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(strPath), typeof(SO_Hats)));
        }
        foreach(Hat hat in soHats.hats)
        {
            hats.Add(hat);
            CreateHatMenu2(hat);
        }
        

    }
    private void CreateHatMenu2(Hat currentHat)
    {
        Transform par = HatMenuPanel.transform;
        GameObject UIButton = Instantiate(ButtonPrefab, par);

        UIButton.GetComponentInChildren<Text>().text = currentHat.title;
        UIButton.GetComponent<Button>().onClick.AddListener(delegate { dino.GetComponent<ApplyHat>().PassNewHat(currentHat.hatType); });
    }
}
