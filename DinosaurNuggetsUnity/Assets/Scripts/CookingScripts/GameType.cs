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
    Flour = 51,
    Milk = 52,
    Lettuce = 53,
    Onion_Whole = 54,
    Steak = 55,
    Fish_Whole = 56,
    Egg = 57,
    Tomato_Whole = 58,
    Cheese = 59,
    Brocolli = 60,
    Chicken = 61,
    Mushroom = 62,
    Strawberry = 63,
    Sugar = 64,
    //----------------------
    Bread_Sliced = 100,
    Onion_Sliced = 101,
    Fish_Sliced = 102,
    Tomato_Sliced = 103,
    Mushroom_Sliced = 104
}

public class GameType : MonoBehaviour
{
    public void AttemptAddToRecipe(Recipe r)
    {
        //r.AddIngredient(this);
    }
}
