using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CharacterSettings : MonoBehaviour
{
    [SerializeField] private HatType currentHat = HatType.Chef;
    [SerializeField] public CharacterColour characterColour = CharacterColour.Blue;
    [SerializeField] private GameObject _matObj = null;

    private SO_ColourSettings dinoSettings = null;
    private string dinoSettingsPath = "Assets/Database/PlayerSettings";
    private Material _mat = null;

    private void Start()
    {
        UpdateSettings();
    }

    private void Update()
    {
        
    }

    private void UpdateSettings()
    {
        /*
        foreach (string strPath in AssetDatabase.FindAssets("t:SO_ColourSettings", new[] { dinoSettingsPath }))
        {
            dinoSettings = (SO_ColourSettings)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(strPath), typeof(SO_ColourSettings));
        }

        switch (characterColour)
        {
            case CharacterColour.Blue:
                _mat = dinoSettings.colourSettings.Find(x => x.characterColour == CharacterColour.Blue)._mat;
                break;
            case CharacterColour.Red:
                _mat = dinoSettings.colourSettings.Find(x => x.characterColour == CharacterColour.Red)._mat;
                break;
        }

        _matObj.GetComponentInChildren<SkinnedMeshRenderer>().material = _mat;
        */
    }
}
