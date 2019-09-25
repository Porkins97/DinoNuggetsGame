using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SManager : MonoBehaviour
{
    public List<SO_Ingredients> ingredientList;
    public List<SO_Recipes> mealList;
    [Header("UI Settings")]
    public GameObject UI;
    public GameObject Player1;
    public GameObject Player2;
    public Texture2D checkmarkUI;



    //-------------

    private string ingredientPath = "Assets/Database/Ingredients";
    private string mealPath = "Assets/Database/Meals";
    public List<GameObject> UIIngredients;
    public int UIIngredientsFinished;
    

    void Start()
    {
        ingredientList = new List<SO_Ingredients>();
        mealList = new List<SO_Recipes>();
        
        foreach (string strPath in AssetDatabase.FindAssets("t:SO_Ingredients", new[] { ingredientPath }))
        {
            ingredientList.Add((SO_Ingredients)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(strPath), typeof(SO_Ingredients)));
        }
        foreach (string strPath in AssetDatabase.FindAssets("t:SO_Recipes", new[] { mealPath }))
        {
            mealList.Add((SO_Recipes)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(strPath), typeof(SO_Recipes)));
        }

        
        SO_Recipes currentRecipe = mealList[2];

        UI.SetActive(true);
        MealToUIStarter(currentRecipe);
    }




    

    private void MealToUIStarter(SO_Recipes currentRecipe)
    {
        UIIngredients = new List<GameObject>();
        foreach(SO_Ingredients ingredient in currentRecipe.ingredients)
        {
            if(ingredient != null)
            {
                string name = String.Format("Ingredient_{0}", ingredient.ingredientName);
                CreateUIImages(name, Player1.transform, ingredient.texture);
                Debug.Log(name);
            }
        }
    }

    private void CreateUIImages(string ingredientName, Transform Player, Texture2D tex2D)
    {
        Transform par = Player.transform.Find(String.Format("{0}_Ingredients", Player.name));
        GameObject n = new GameObject();
        n.transform.SetParent(par, false);
        n.name = ingredientName;
        n.AddComponent<CanvasRenderer>();
        RawImage image = n.AddComponent<RawImage>();
        image.texture = tex2D;
        n.layer = LayerMask.NameToLayer("UI");
        UIIngredients.Add(n);
    }

    private void FinishUIImages(SO_Ingredients currentIngredient)
    {
        if(currentIngredient == UIIngredients[UIIngredientsFinished])
        {

            //checkmarkUI
        }
    }
}
