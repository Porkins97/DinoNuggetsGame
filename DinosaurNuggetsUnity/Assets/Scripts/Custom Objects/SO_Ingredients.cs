using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "", menuName = "Dino Nuggets/Ingredients")]
public class SO_Ingredients : ScriptableObject
{
    public string ingredientName;
    public GameObject ingredientPrefab;
    public Texture2D texture;
    public IngredientType type;
    public bool Spawnable = true;
    public SO_Ingredients unslicedVersion;
    public SO_Ingredients slicedVersion;
}
