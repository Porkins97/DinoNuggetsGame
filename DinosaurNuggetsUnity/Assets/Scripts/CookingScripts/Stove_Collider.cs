using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove_Collider : MonoBehaviour
{
    private Stove stoveScript;
    private DinoSceneManager sManager;

    void Start()
    {
        if (sManager == null)
        {
            sManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DinoSceneManager>();
        }
        if(stoveScript == null)
        {
            stoveScript = GetComponentInParent<Stove>();
        }
        

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Utensil" || col.tag == "Ingredient")
        {
            if (col.GetComponent<ItemAttributes>().beingUsed == false)
            {
                IngredientType iType = col.GetComponent<ItemAttributes>().GameType;                
                if((int)iType < 50)
                {
                    //Utensil
                    if(col.GetComponent<ItemAttributes>().Burnable == true)
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
        if (col.tag == "Utensil" || col.tag == "Ingredient")
        {
            if (col.GetComponent<ItemAttributes>().beingUsed == false)
            {
                IngredientType iType = col.GetComponent<ItemAttributes>().GameType;
                if ((int)iType < 50)
                {
                    //Utensil
                    if (col.GetComponent<ItemAttributes>().Burnable == true)
                    {
                        stoveScript.Burn(col.gameObject);
                    }
                    else
                    {
                        stoveScript.Placed(col.gameObject);
                    }

                }
                else if ((int)iType >= 50)
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
            if (col.GetComponent<ItemAttributes>().beingUsed == true)
            {
                stoveScript.Removed(col.gameObject);
            }
        }
    }
}
