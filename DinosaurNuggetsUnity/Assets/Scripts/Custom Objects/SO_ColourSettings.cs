using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "", menuName = "Dino Nuggets/Colour Settings")]
public class SO_ColourSettings : ScriptableObject
{
    [SerializeField] public List<ColourSettings> colourSettings;
}
[System.Serializable]
public class ColourSettings
{
    public string title;
    public Material _mat;
    public CharacterColour characterColour;
}