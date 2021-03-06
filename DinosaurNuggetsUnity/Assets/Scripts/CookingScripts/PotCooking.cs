﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PotCooking : MonoBehaviour
{
    [SerializeField] Players _currentPlayer;
    [SerializeField] public Collider currentCol = null;
    private DinoSceneManager _DSM;
    DinoPlayerSettings player;
    DinoPlayerSettings player_other;

    // Start is called before the first frame update
    void Start()
    {
        if (_DSM == null)
        {
            _DSM = GameObject.FindGameObjectWithTag("Manager").GetComponent<DinoSceneManager>();
        }

        if (_currentPlayer == Players.Player1)
        {
            player = _DSM.DinoPlayer1;
            player_other = _DSM.DinoPlayer2;
        }
        else
        {
            player = _DSM.DinoPlayer2;
            player_other = _DSM.DinoPlayer1;
        }
    }


    private void CookIngredient(GameObject obj)
    {
        IngredientType currentItem = obj.GetComponent<ItemAttributes>().GameType;
        SO_Ingredients ingredient = player.playerRecipe.ingredients.Find(x => x.type == currentItem);

        if (obj.GetComponent<ItemAttributes>().lastPlayer == _currentPlayer)
        {
            if (ingredient == player.playerIngredientList[player.playerRecipeDone])
            {
                obj.GetComponent<ItemAttributes>().beingUsed = true;
                player.dinoPlayer.GetComponent<PickupScript>().AllItems.Remove(obj);
                _DSM.FinishUIImages(ingredient, player);
                Destroy(obj);
                player.playerRecipeDone++;
                _DSM.CheckIfWon(player);
            }
        }
        //Debug.Log("CurrentIngredient " + ingredient);
        //Debug.Log("PlayerIngred " + player.playerIngredientList[player.playerRecipeDone]);
       
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ingredient")
        {
            if (col.GetComponent<ItemAttributes>().beingUsed == false)
            {
                CookIngredient(col.gameObject);
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Ingredient")
        {
            if (col.GetComponent<ItemAttributes>().beingUsed == false)
            {
                CookIngredient(col.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        
    }
}
