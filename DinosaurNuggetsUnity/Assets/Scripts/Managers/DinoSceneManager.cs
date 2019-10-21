using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class DinoSceneManager : MonoBehaviour
{
    
    [Header("UI Settings")]
    public GameObject UI;
    public GameObject Player1;
    public GameObject Player2;
    public Texture2D checkmarkUI;
    public Canvas pauseCanvas = null;
    public Canvas gameCanvas = null;
    public int seed;

    [Header("User Settings")]
    [SerializeField] public List<UserInputs> allUsers = new List<UserInputs>();
    private bool gamePaused = false;
    
    //-------------

    private string ingredientPath = "Assets/Database/Ingredients";
    private string mealPath = "Assets/Database/Meals";
    private string utensilPath = "Assets/Database/Utensils";
    public List<GameObject> UIIngredients;
    public int UIIngredientsFinished;
    public List<SO_Ingredients> currentIngredientList;
    public List<SO_Ingredients> ingredientList;
    public List<SO_Utensils> utensilList;
    public List<SO_Recipes> mealList;


    void Awake()
    {



        pauseCanvas.enabled = false;
        gameCanvas.enabled = true;

        //Ingredients start.
        ingredientList = new List<SO_Ingredients>();
        mealList = new List<SO_Recipes>();
        utensilList = new List<SO_Utensils>();
        
        foreach (string strPath in AssetDatabase.FindAssets("t:SO_Ingredients", new[] { ingredientPath }))
        {
            ingredientList.Add((SO_Ingredients)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(strPath), typeof(SO_Ingredients)));
        }
        foreach (string strPath in AssetDatabase.FindAssets("t:SO_Recipes", new[] { mealPath }))
        {
            mealList.Add((SO_Recipes)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(strPath), typeof(SO_Recipes)));
        }
        foreach (string strPath in AssetDatabase.FindAssets("t:SO_Utensils", new[] { utensilPath }))
        {
            utensilList.Add((SO_Utensils)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(strPath), typeof(SO_Utensils)));
        }

        SO_Recipes currentRecipe = mealList[(int)UnityEngine.Random.Range(0, mealList.Count-1)];
        UI.SetActive(true);
        MealToUIStarter(currentRecipe);
    }

    public void PauseGame()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f;
            gamePaused = true;
            Debug.Log("Paused");
            gameCanvas.enabled = false;
            pauseCanvas.enabled = true;
            foreach(UserInputs _inputs in allUsers)
            {
                _inputs.current_Actions.Disable();
                _inputs.current_UI.Enable();
            }
        }
        else
        {
            Time.timeScale = 1f;
            gamePaused = false;
            Debug.Log("UnPaused");
            pauseCanvas.enabled = false;
            gameCanvas.enabled = true;
            foreach (UserInputs _inputs in allUsers)
            {
                _inputs.current_Actions.Enable();
                _inputs.current_UI.Disable();
            }
        }
    }



#region UIHelpers
    //-----------------------------------------------------------------------------
    //UI Helpers
    //-----------------------------------------------------------------------------

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
            Transform par = UIIngredients[UIIngredientsFinished].transform;
            GameObject UICheckboxElement = new GameObject();
            UICheckboxElement.transform.SetParent(par, false);
            UICheckboxElement.name = String.Format("{0}_Checked", UIIngredients[UIIngredientsFinished].name);
            UICheckboxElement.AddComponent<CanvasRenderer>();
            RawImage image = UICheckboxElement.AddComponent<RawImage>();
            UICheckboxElement.GetComponent<RectTransform>().sizeDelta = new Vector2(par.GetComponent<RectTransform>().rect.width, par.GetComponent<RectTransform>().rect.height);
            image.texture = checkmarkUI;
            UICheckboxElement.layer = LayerMask.NameToLayer("UI");
            UIIngredientsFinished++;
    }

    //-----------------------------------------------------------------------------
    //Ingredient Checkers
    //-----------------------------------------------------------------------------

    public void runThrough(GameObject item)
    {
        IngredientType currentItem = item.GetComponent<BeingUsed>().GameType;
        SO_Ingredients ingredient = ingredientList.Find(x => x.type == currentItem);
        if(ingredient == currentIngredientList[UIIngredientsFinished])
            CorrectIngredient(ingredient, item);
    }

    private void CorrectIngredient(SO_Ingredients ingredient, GameObject item)
    {
        FinishUIImages(ingredient);
        Destroy(item);
    }
#endregion
    
}
