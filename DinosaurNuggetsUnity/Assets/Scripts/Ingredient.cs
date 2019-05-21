using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum IngredientType
{
    BREAD,
    FLOUR,
    MILK,
    LETTUCE,
    ONION_WHOLE,
    ONION_CHOPPED,
    STEAK,
    FISH,
    EGG,
    TOMATO_WHOLE,
    TOMATO_CHOPPED,
    CHEESE,
    BROCOLLI,
    CHICKEN
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
