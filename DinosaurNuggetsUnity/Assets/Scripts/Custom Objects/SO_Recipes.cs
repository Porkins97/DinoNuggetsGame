using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "", menuName = "Dino Nuggets/Meals")]
public class SO_Recipes : ScriptableObject
{
    public List<SO_Ingredients> ingredients;
    public GameObject meal;
}
