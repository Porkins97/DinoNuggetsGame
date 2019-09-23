using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeStep
{
    public int amountNeeded;
    private int amountProvided = 0;
    //public IngredientType ingredient;
    bool is_complete = false;

    public void ResetStep()
    {
        MarkComplete(false);
        amountProvided = 0;
    }

    public void MarkComplete(bool val)
    {
        is_complete = val;
    }

    public bool IsComplete()
    {
        return is_complete;
    }

    //returns true if adding the ingredient was OK.
    //returns false if there's a problem
    /* 
    public bool AddIngredient(IngredientType itype)
    {
        bool retval = false;
        if (itype == ingredient)
        {
            if (amountProvided < amountNeeded)
            {
                amountProvided += 1;
                if (amountProvided == amountNeeded)
                {
                    MarkComplete(true);
                }
                retval = true;
            }
        }
        return retval;
    }*/
}

public class Recipe : MonoBehaviour
{
    public GameObject Ingredient;
    public GameObject Pot;
    public GameObject Burner;
    public GameObject UICamera;
    
    public RecipeStep[] steps;
    private int currentStep;
    public float progress;

    // Start is called before the first frame update
    void Start()
    {
        Reset();   
    }

    
    public void Reset()
    {
        foreach (RecipeStep rs in steps)
        {
            rs.ResetStep();
        }
        currentStep = 0;
        progress = 0.0f; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    RecipeStep GetCurrentStep()
    {
        return steps[currentStep];
    }

    float UpdateProgress()
    {
        if (steps.Length > 0)
            progress = currentStep / (1.0f * steps.Length);
        Debug.Log("Progress is " + progress * 100.0f + "%");
        if (progress == 1)
        {
            UICamera.GetComponent<CameraUIScript>().WinUI();
            Debug.Log("Should show win screen now");
        }
        return progress;
    }
/* 
    public bool AddIngredient(Ingredient ingredient)
    {
        bool correct = GetCurrentStep().AddIngredient(ingredient.itype);
        if (correct)
        {
            Debug.Log("added successfully");
            if (GetCurrentStep().IsComplete())
            {
                Debug.Log("This Step is Complete");
                currentStep += 1;
                UpdateProgress();
            }
        }
        else
        {
            Debug.Log("Recipe Failed");
            Reset();
        }
        return correct;
    }*/
}
