﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum IngredientType
{
    Pot = 0,
    Pan = 1,
    Cutting_Board = 2,
    Knife_A = 3,
    Knife_Butcher = 4,
    Fire_Extinguisher = 5,
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

[System.Serializable]
public enum HatType
{
    None,
    Chef,
    Crown,
    Cowboy,
    Helicopter,
    Party,
    //SmallRussian,
    LargeRussian,
    Shark,
    Snapback,
    SpaceHelmet,
    SmallTophat,
    LargeTophat,
    
}

[System.Serializable]
public enum CharacterColour
{
    Blue,
    Red
}

public enum Players
{
    Player1, Player2
}

public enum SpawnRole
{ 
    SpawnA,  SpawnB 
}

[System.Serializable]
public struct DinoPlayerSettings
{
    public Players player;
    public SO_Recipes playerRecipe;
    public List<SO_Ingredients> playerIngredientList;
    public List<GameObject> playerUIIngredients;
    public GameObject playerObj;
    public GameObject dinoPlayer;
    public int playerRecipeDone;
   

    public DinoPlayerSettings(Players _player,
                            SO_Recipes _playerRecipe,
                            List<GameObject> _playerUIIngredients,
                            List<SO_Ingredients> _playerIngredientList,
                            GameObject _playerObj,
                            GameObject _dinoPlayer,
                            int _playerRecipeDone = 0)
    {
        player = _player;
        playerRecipe = _playerRecipe;
        playerObj = _playerObj;
        playerRecipeDone = _playerRecipeDone;
        playerIngredientList = _playerIngredientList;
        playerUIIngredients = _playerUIIngredients;
        dinoPlayer = _dinoPlayer;
}
}

public static class EnumLibrary
{
    public static int IngredientsOverAll(GameObject x, GameObject y)
    {
        if (x == null)
        {
            if (y == null)
            {
                return 0; // If x is null and y is null, they're equal.
            }
            else
            {
                return -1; // If x is null and y is not null, y is greater. 
            }
        }
        else
        {
            // If x is not null... and y is null, x is greater.
            if (y == null)
            {
                return 1;
            }
            else
            {
                int x_enum = (int)x.GetComponent<ItemAttributes>().GameType;
                int y_enum = (int)y.GetComponent<ItemAttributes>().GameType;

                if (x_enum < 50) // x=utensil
                {
                    if (y_enum < 50) // x=y=utensil
                    {
                        return 0;
                    }
                    else // x=utensil y=ingredient
                    {
                        return 1;
                    }
                }
                else // x=ingredient
                {
                    if (y_enum < 50) // x=ingredient y=utensil
                    {
                        return -1;
                    }
                    else // x=y=ingredient
                    {
                        return 0;
                    }
                }

            }
        }
    }
}