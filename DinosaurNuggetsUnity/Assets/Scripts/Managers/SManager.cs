using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SManager : MonoBehaviour
{
    
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
    public List<SO_Ingredients> currentIngredientList;
    public List<SO_Ingredients> ingredientList;
    public List<SO_Recipes> mealList;


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
        
        SO_Recipes currentRecipe = mealList[1];

        UI.SetActive(true);
        MealToUIStarter(currentRecipe);
    }

    private void MealToUIStarter(SO_Recipes currentRecipe)
    {
        UIIngredients = new List<GameObject>();
        currentIngredientList = new List<SO_Ingredients>();
        UIIngredientsFinished = 0;

        foreach (SO_Ingredients ingredient in currentRecipe.ingredients)
        {
            if(ingredient != null)
            {
                string name = String.Format("Ingredient_{0}", ingredient.ingredientName);
                CreateUIImages(name, Player1.transform, ingredient.texture, ingredient);
            }
        }
    }

    private void CreateUIImages(string ingredientName, Transform Player, Texture2D tex2D, SO_Ingredients ingredient)
    {
        Transform par = Player.transform.Find(String.Format("{0}_Ingredients", Player.name));
        GameObject UIImage = new GameObject();
        UIImage.transform.SetParent(par, false);
        UIImage.name = ingredientName;
        UIImage.AddComponent<CanvasRenderer>();
        RawImage image = UIImage.AddComponent<RawImage>();
        image.texture = tex2D;
        UIImage.layer = LayerMask.NameToLayer("UI");

        UIIngredients.Add(UIImage);
        currentIngredientList.Add(ingredient);
    }

    public void FinishUIImages(SO_Ingredients currentIngredient)
    {
        if(currentIngredient == currentIngredientList[UIIngredientsFinished])
        {
            Transform par = UIIngredients[UIIngredientsFinished].transform;
            GameObject UICheckboxElement = new GameObject();
            UICheckboxElement.transform.SetParent(par, false);
            UICheckboxElement.name = String.Format("{0}_Checked", UIIngredients[UIIngredientsFinished].name);
            UICheckboxElement.AddComponent<CanvasRenderer>();
            RawImage image = UICheckboxElement.AddComponent<RawImage>();
            UICheckboxElement.GetComponent<RectTransform>().sizeDelta = new Vector2(par.GetComponent<RectTransform>().rect.width, par.GetComponent<RectTransform>().rect.height);
            image.texture = checkmarkUI;
            UICheckboxElement.layer = LayerMask.NameToLayer("UI");
        }
    }


    public void runThrough(GameObject item)
    {
        IngredientType currentItem = item.GetComponent<BeingUsed>().GameType;
        SO_Ingredients ingredient = ingredientList.Find(x => x.type == currentItem);
        CorrectIngredient(ingredient, item);
        

        switch (currentItem)
        {
            case IngredientType.Bread_Sliced:
                
                break;
            case IngredientType.Bread_Whole:
                break;
            case IngredientType.Brocolli:
                break;
            case IngredientType.Cheese:
                break;
            case IngredientType.Chicken:
                break;
            case IngredientType.Egg:
                break;
            case IngredientType.Fish_Sliced:
                break;
            case IngredientType.Fish_Whole:
                break;
            case IngredientType.Flour:
                break;
            case IngredientType.Lettuce:
                break;
            case IngredientType.Milk:
                break;
            case IngredientType.Mushroom:
                break;
            case IngredientType.Mushroom_Sliced:
                break;
            case IngredientType.Onion_Sliced:
                break;
            case IngredientType.Onion_Whole:
                break;
            case IngredientType.Steak:
                break;
            case IngredientType.Tomato_Sliced:
                break;
            case IngredientType.Tomato_Whole:
                break;
        }
    }

    private void CorrectIngredient(SO_Ingredients ingredient, GameObject item)
    {
        Debug.Log(ingredient);
        Debug.Log(item);
        FinishUIImages(ingredient);
        Destroy(item);
        UIIngredientsFinished++;
    }
    
}
