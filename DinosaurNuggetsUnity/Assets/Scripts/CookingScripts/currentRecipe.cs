using System;
using UnityEngine;

public class CurrentRecipe : SManager
{

    public void runThrough(GameObject item)
    {
        IngredientType currentItem = item.GetComponent<BeingUsed>().GameType;
        SO_Ingredients ingredient = ingredientList.Find(x => x.type == currentItem);
        CorrectIngredient(ingredient, item);
        Debug.Log(ingredient);
        Debug.Log(item);

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
        FinishUIImages(ingredient);
        Destroy(item);
        UIIngredientsFinished++;
    }
}
