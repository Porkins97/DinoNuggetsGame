using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "", menuName = "Dino Nuggets/Utensils")]
public class SO_Utensils : ScriptableObject
{
    public string utensilName;
    public GameObject utensilPrefab;
    public IngredientType type;
}
