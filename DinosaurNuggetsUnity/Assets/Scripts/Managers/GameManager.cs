using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SO_Ingredients[] ingredientArray;
    public SO_Utensils[] utensilsArray;
    public SO_Recipes[] recipesArray;

    public Players playerWon;
    
    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
