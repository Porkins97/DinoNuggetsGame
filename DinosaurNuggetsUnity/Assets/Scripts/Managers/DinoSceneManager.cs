using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DinoSceneManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Main user Settings.
    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("UI Settings")]
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private Texture2D checkmarkUI;
    [SerializeField] private Canvas gameCanvas = null;
    [SerializeField] private Canvas pauseCanvas = null;
    [SerializeField] private GameObject pauseButtonSelected = null;

    
    
    [SerializeField] private int seed;


    [Header("User Settings")]
    [SerializeField] private InputSystemUIInputModule uiInput = null;
    [SerializeField] public List<UserInputs> allUsers = new List<UserInputs>();

    [Header("Spawn Settings")]
    [SerializeField] public Transform SpawnTransform = null;
    
    private bool gamePaused = false;
    public int userPaused = 0;
    private InputActionAsset defaultAsset;

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Main user Settings.
    /////////////////////////////////////////////////////////////////////////////////////////////////////////


    private EventSystem eventSystem;
    [SerializeField] public DinoPlayerSettings DinoPlayer1;
    [SerializeField] public DinoPlayerSettings DinoPlayer2;

    private string ingredientPath = "Assets/Database/Ingredients";
    private string mealPath = "Assets/Database/Meals";
    private string utensilPath = "Assets/Database/Utensils";
    private List<GameObject> UIIngredients;
    private int UIIngredientsFinished;
    [HideInInspector] public List<SO_Ingredients> currentIngredientList;
    [HideInInspector] public List<SO_Ingredients> ingredientList;
    [HideInInspector] public List<SO_Utensils> utensilList;
    [HideInInspector] public List<SO_Recipes> mealList;


    void Awake()
    {
        UI.SetActive(true);
        pauseCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(true);

        eventSystem = uiInput.GetComponent<EventSystem>();
        defaultAsset = uiInput.actionsAsset;

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
        //SO_Recipes currentRecipe = mealList[1];
        foreach(UserInputs input in allUsers)
        {
            if (input.player == Players.Player1)
            {
                DinoPlayer1 = new DinoPlayerSettings(Players.Player1, currentRecipe, new List<GameObject>(), new List<SO_Ingredients>(), Player1, input.playerObj, 0);
            }
            else if (input.player == Players.Player2)
            {
                DinoPlayer2 = new DinoPlayerSettings(Players.Player2, currentRecipe, new List<GameObject>(), new List<SO_Ingredients>(), Player2, input.playerObj, 0);
            }
        }
        
        MealToUIStarter(DinoPlayer1, DinoPlayer2, currentRecipe);

        //SpawnItems(currentRecipe);

        
    }

    private void Start()
    {
        uiInput.DisableAllActions();
    }

    public void PauseGame()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f;
            Debug.Log("Paused");
            gameCanvas.enabled = false;
            pauseCanvas.enabled = true;
            foreach (UserInputs _inputs in allUsers)
            {
                _inputs.current_Actions.Disable();
                _inputs.current_UI.Enable();
                uiInput.EnableAllActions();
            }
            gamePaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("UnPaused");
            pauseCanvas.enabled = false;
            gameCanvas.enabled = true;
            foreach (UserInputs _inputs in allUsers)
            {
                _inputs.current_Actions.Enable();
                _inputs.current_UI.Disable();
                uiInput.DisableAllActions();
            }
            gamePaused = false;
        }
        
    }
    


    private void SetUIAgain(InputActionAsset userAsset)
    {
        uiInput.submit.Set(userAsset.FindActionMap("UI").FindAction("Submit"));
        Debug.Log(userAsset.FindActionMap("UI").FindAction("Submit"));
        Debug.Log(uiInput.submit.action);
        uiInput.move.Set(userAsset.FindActionMap("UI").FindAction("Navigate"));
        uiInput.cancel.Set(userAsset.FindActionMap("UI").FindAction("Cancel"));
    }
    


    
    public void TimeUp()
    {
        SceneManager.LoadScene("TimesUp");
    }


    //-----------------------------------------------------------------------------
    //Spawn Helpers
    //-----------------------------------------------------------------------------

    #region SpawnHelpers
    private void SpawnItems(SO_Recipes currentRecipe)
    {
        //Make our listed ingredients. Needed ingredients are those in the meal, and we make sure the left over spaces are filled.
        List<SO_Ingredients> neededIngredients = currentRecipe.ingredients;
        List<SO_Ingredients> leftIngredients = ingredientList;

        //Grab all spawner objects.
        List<GameObject> Spawners = new List<GameObject>();
        Spawners.AddRange(GameObject.FindGameObjectsWithTag("ItemRespawner"));

        //Randomize the order of our spawners, so its different <= May not be working?
        Spawners.OrderBy(x => (new System.Random().Next()));

        //Find all ingredients in the ingredient list, and remove any duplicates.
        for (int ii = 0; ii < neededIngredients.Count; ii++)
        {
            if(neededIngredients[ii].Spawnable == false)
            {
                neededIngredients[ii] = neededIngredients[ii].unslicedVersion;
            }
            SO_Ingredients foundIngredient = leftIngredients.Find(x => x == neededIngredients[ii]);
            leftIngredients.Remove(foundIngredient);
        }

        for (int ii = 0; ii < leftIngredients.Count; ii++)
        {
            if (leftIngredients[ii].Spawnable == false)
            {
                leftIngredients.RemoveAt(ii);
            }
        }
        
        //Remove final Duplicates
        neededIngredients = neededIngredients.Distinct().ToList();
        leftIngredients = leftIngredients.Distinct().ToList();

        for (int ii = 0; ii < neededIngredients.Count; ii++)
        {
            if(Spawners.Count != 0)
            {
                GameObject spawnPoint = Spawners[0];
                Instantiate(neededIngredients[ii].ingredientPrefab, spawnPoint.transform.position, Quaternion.identity, SpawnTransform);
                Spawners.Remove(spawnPoint);
            }
        }

        for (int ii = 0; ii < leftIngredients.Count; ii++)
        {
            if (Spawners.Count != 0)
            {
                GameObject spawnPoint = Spawners[0];
                Instantiate(leftIngredients[ii].ingredientPrefab, spawnPoint.transform.position, Quaternion.identity, SpawnTransform);
                Spawners.Remove(spawnPoint);
            }
        }


    }
#endregion


    //-----------------------------------------------------------------------------
    //UI Helpers
    //-----------------------------------------------------------------------------

    #region UIHelpers


    private void MealToUIStarter(DinoPlayerSettings _player1Settings, DinoPlayerSettings _player2Settings, SO_Recipes currentRecipe)
    {
        foreach (SO_Ingredients ingredient in currentRecipe.ingredients)
        {
            if (ingredient != null)
            {
                string name = String.Format("Ingredient_{0}", ingredient.ingredientName);
                CreateUIImages(name, _player1Settings.playerObj.transform, ingredient.texture, ingredient, _player1Settings);
                CreateUIImages(name, _player2Settings.playerObj.transform, ingredient.texture, ingredient, _player2Settings);
            }
        }
    }

    private void CreateUIImages(string ingredientName, Transform Player, Texture2D tex2D, SO_Ingredients ingredient, DinoPlayerSettings _player)
    {
        Transform par = Player.transform.Find(String.Format("{0}_Ingredients", Player.name));
        GameObject UIImage = new GameObject();
            
        UIImage.transform.SetParent(par, false);
        UIImage.name = ingredientName;
        UIImage.AddComponent<CanvasRenderer>();
        RawImage image = UIImage.AddComponent<RawImage>();
        image.texture = tex2D;
        UIImage.layer = LayerMask.NameToLayer("UI");

        _player.playerUIIngredients.Add(UIImage);
        _player.playerIngredientList.Add(ingredient);
    }


    
    public void FinishUIImages(SO_Ingredients currentIngredient, DinoPlayerSettings _player)
    {
            Transform par = _player.playerUIIngredients[_player.playerRecipeDone].transform;
            GameObject UICheckboxElement = new GameObject();
            UICheckboxElement.transform.SetParent(par, false);
            UICheckboxElement.name = String.Format("{0}_Checked", _player.playerUIIngredients[_player.playerRecipeDone].name);
            UICheckboxElement.AddComponent<CanvasRenderer>();
            RawImage image = UICheckboxElement.AddComponent<RawImage>();
            UICheckboxElement.GetComponent<RectTransform>().sizeDelta = new Vector2(par.GetComponent<RectTransform>().rect.width, par.GetComponent<RectTransform>().rect.height);
            image.texture = checkmarkUI;
            UICheckboxElement.layer = LayerMask.NameToLayer("UI");
    }

    #endregion





    #region WinnerChecks

    public void CheckIfWon(DinoPlayerSettings _player)
    {
        if(_player.playerRecipeDone == _player.playerRecipe.ingredients.Count)
        {
            SceneManager.LoadScene("WinScreen");
        }
        
    }

    #endregion

}

public class UserInputs
{
    public InputUser currentUser;
    public InputActionAsset current_Asset;
    public InputActionMap current_UI;
    public InputActionMap current_Actions;
    public GameObject playerObj;
    public Players player;


    public UserInputs(InputUser _currentUser, InputActionMap _current_UI, InputActionMap _current_Actions, InputActionAsset _current_Asset, GameObject _playerObj, Players _player)
    { 
        this.currentUser = _currentUser;
        this.current_UI = _current_UI;
        this.current_Actions = _current_Actions;
        this.current_Asset = _current_Asset;
        this.playerObj = _playerObj;
        this.player = _player;
    }
}
