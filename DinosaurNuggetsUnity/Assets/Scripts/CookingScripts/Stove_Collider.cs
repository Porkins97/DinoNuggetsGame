using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove_Collider : MonoBehaviour
{
    [SerializeField] private Stove stoveScript;
    [SerializeField] private SManager sManager;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Utensil" || col.tag == "Ingredient")
        {
            if (col.GetComponent<BeingUsed>().beingUsed == false)
            {
                IngredientType iType = col.GetComponent<BeingUsed>().GameType;                
                if((int)iType < 50)
                {
                    //Utensil
                    if(col.GetComponent<BeingUsed>().Burnable == true)
                    {
                        stoveScript.Burn(col.gameObject);
                    }
                    else
                    {
                        stoveScript.Placed(col.gameObject);
                    }

                }
                else if((int)iType >= 50)
                {
                    //Ingredients
                    sManager.runThrough(col.gameObject);
                }
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Utensil" || col.tag == "Ingredient")
        {
            if (col.GetComponent<BeingUsed>().beingUsed == false)
            {
                IngredientType iType = col.GetComponent<BeingUsed>().GameType;

                if((int)iType < 50)
                {
                    //Utensil
                    if(col.GetComponent<BeingUsed>().Burnable == true)
                    {
                        stoveScript.Placed(col.gameObject);
                        stoveScript.Burn(col.gameObject);
                    }
                    else
                    {
                        stoveScript.Placed(col.gameObject);
                    }
                        
                }else if((int)iType >= 50)
                {
                    //Ingredients
                    sManager.runThrough(col.gameObject);
                }
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Utensil" || col.tag == "Ingredient")
        {
            if (col.GetComponent<BeingUsed>().beingUsed == true)
            {
                stoveScript.Removed(col.gameObject);
            }
        }
    }
}
