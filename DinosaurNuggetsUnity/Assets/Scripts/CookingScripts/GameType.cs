using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum IngredientType
{
    Pot = 0,
    Pan,
    Cutting_Board,
    //-----------------------
    Bread_Whole = 50,
    Bread_Sliced,
    Flour,
    Milk,
    Lettuce,
    Onion_Whole,
    Onion_Sliced,
    Steak,
    Fish_Whole,
    Fish_Sliced,
    Egg,
    Tomato_Whole,
    Tomato_Sliced,
    Cheese,
    Brocolli,
    Chicken,
    Mushroom,
    Mushroom_Sliced
}

public class GameType : MonoBehaviour
{
    public IngredientType iType;
    public void AttemptAddToRecipe(Recipe r)
    {
        //r.AddIngredient(this);
    }
}
