using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "", menuName = "Dino Nuggets/Hats")]
public class SO_Hats : ScriptableObject
{
    [FormerlySerializedAs("hatName")]
    [SerializeField] public List<Hat> hats;
}
[System.Serializable]
public class Hat
{
    public string title;
    public GameObject hatPrefab;
    public HatType hatType;
    
}