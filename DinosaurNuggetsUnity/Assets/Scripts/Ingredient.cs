using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum IngredientType
{
    BREAD_WHOLE,
    BREAD_SLICED,
    FLOUR,
    MILK,
    LETTUCE,
    ONION_WHOLE,
    ONION_SLICED,
    STEAK,
    FISH_WHOLE,
    FISH_SLICED,
    EGG,
    TOMATO_WHOLE,
    TOMATO_SLICED,
    CHEESE,
    BROCOLLI,
    CHICKEN,
    MUSHROOM,
    MUSHROOM_SLICED
}

public class Ingredient : MonoBehaviour
{

    public IngredientType itype;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttemptAddToRecipe(Recipe r)
    {
        r.AddIngredient(this);
    }
}
