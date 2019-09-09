﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "", menuName = "Dino Nuggets/Meals")]
public class SO_Recipes : ScriptableObject
{
    public List<GameObject> ingredients;
    public GameObject meal;
}
