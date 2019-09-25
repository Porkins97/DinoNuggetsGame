using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum IngredientType
{
    Pot = 0,
    Pan = 1,
    Cutting_Board = 2,
    Knife_A = 3,
    Knife_Butcher = 4,
    //-----------------------
    Bread_Whole = 50,
    Bread_Sliced = 51,
    Flour = 52,
    Milk = 53,
    Lettuce = 54,
    Onion_Whole = 55,
    Onion_Sliced = 56,
    Steak = 57,
    Fish_Whole = 58,
    Fish_Sliced = 59,
    Egg = 60,
    Tomato_Whole = 61,
    Tomato_Sliced = 62,
    Cheese = 63,
    Brocolli = 64,
    Chicken = 65,
    Mushroom = 66,
    Mushroom_Sliced = 67
}

public class GameType : MonoBehaviour
{
    public void AttemptAddToRecipe(Recipe r)
    {
        //r.AddIngredient(this);
    }
}
