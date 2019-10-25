using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class DinoSpawnItems : MonoBehaviour
{

    public enum AmountofSpawns {Two, Three}

    [HideInInspector] public AmountofSpawns amountOfSpawns;
    [HideInInspector] public IngredientType ingredientA = IngredientType.Bread_Whole;
    [HideInInspector] public IngredientType ingredientB = IngredientType.Brocolli;
    [HideInInspector] public IngredientType ingredientC = IngredientType.Cheese;

    private List<Transform> SpawnPoints2 = new List<Transform>();
    private List<Transform> SpawnPoints3 = new List<Transform>();
    [SerializeField] public List<GameObject> currentlySpawned = new List<GameObject>();

    [HideInInspector] public Transform twoSpawns = null;
    [HideInInspector] public Transform threeSpawns = null;
    public Transform ingredientsParents = null;

    private string ingredientPath = "Assets/Database/Ingredients";
    private string utensilPath = "Assets/Database/Utensils";
    private List<SO_Ingredients> ingredientList;
    private List<SO_Utensils> utensilList;


    public void Start()
    {
        if (twoSpawns == null || threeSpawns == null)
        {
            twoSpawns = gameObject.transform.Find("CuttingSpots_2");
            threeSpawns = gameObject.transform.Find("CuttingSpots_3");
        }
        StartingMethods();
    }

    public void StartingMethods()
    {
        CheckForIngredients();

        foreach (Transform child in twoSpawns) { SpawnPoints2.Add(child); }
        foreach (Transform child in threeSpawns) { SpawnPoints3.Add(child); }

        ingredientList = new List<SO_Ingredients>();
        utensilList = new List<SO_Utensils>();
        currentlySpawned = new List<GameObject>();

        
        
        foreach (string strPath in AssetDatabase.FindAssets("t:SO_Ingredients", new[] { ingredientPath }))
        {
            ingredientList.Add((SO_Ingredients)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(strPath), typeof(SO_Ingredients)));
        }
        foreach (string strPath in AssetDatabase.FindAssets("t:SO_Utensils", new[] { utensilPath }))
        {
            utensilList.Add((SO_Utensils)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(strPath), typeof(SO_Utensils)));
        }
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnItems()
    {
        if(ingredientsParents == null) { ingredientsParents = new GameObject("IngredientParents").transform; }

        CheckForIngredients();

        if (amountOfSpawns == AmountofSpawns.Two)
        {
            Spawner(ingredientA, SpawnPoints2[0]);
            Spawner(ingredientB, SpawnPoints2[1]);
        }
        else //AmountofSpawns.Three
        {
            Spawner(ingredientA, SpawnPoints3[0]);
            Spawner(ingredientB, SpawnPoints3[1]);
            Spawner(ingredientC, SpawnPoints3[2]);
        }
    }

    private void Spawner(IngredientType _ingredient, Transform spawnPoint)
    {
        if ((int)_ingredient >= 50)
        {
            
            currentlySpawned.Add(Instantiate(
               ingredientList.Find(x => x.type == _ingredient).ingredientPrefab,
               spawnPoint.position, Quaternion.identity, ingredientsParents));
        }
        else if((int)_ingredient < 50)
        {
            currentlySpawned.Add(Instantiate(
                utensilList.Find(x => x.type == _ingredient).utensilPrefab,
                spawnPoint.position, spawnPoint.rotation, ingredientsParents));
        }
        
    }

    private void CheckForIngredients()
    {
        if (currentlySpawned.Count != 0)
        {
            currentlySpawned.ForEach(x => DestroyImmediate(x));
            currentlySpawned.RemoveRange(0, currentlySpawned.Count);
        }
    }

    public void UpdateMethods()
    {

        bool twoState = amountOfSpawns == AmountofSpawns.Two ? true : false;
        twoSpawns.gameObject.SetActive(twoState);
        threeSpawns.gameObject.SetActive(!twoState);
    }
}