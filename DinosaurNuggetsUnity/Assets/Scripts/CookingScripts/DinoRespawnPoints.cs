using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoRespawnPoints : MonoBehaviour
{
    public DinoRespawn respawn = null;
    
    public SpawnRole role;


    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Ingredient" || col.tag == "Utensil")
        {
            if(role == SpawnRole.SpawnA)
            {
                if(respawn.initialSpawnA == false)
                {
                    //respawn.IngredATransform = col.gameObject.transform;
                    respawn.IngredA_Pos = col.gameObject.transform.position;
                    respawn.IngredA_Rot = col.gameObject.transform.rotation;
                    respawn.IngredientA = col.GetComponent<ItemAttributes>().GameType;
                    respawn.initialSpawnA = true;
                }
                respawn.PointAList.Add(col.gameObject);
            }
            else
            {
                if(respawn.initialSpawnB == false)
                {
                    //respawn.IngredBTransform = col.gameObject.transform;
                    respawn.IngredB_Pos = col.gameObject.transform.position;
                    respawn.IngredB_Rot = col.gameObject.transform.rotation;
                    respawn.IngredientB = col.GetComponent<ItemAttributes>().GameType;
                    respawn.initialSpawnB = true;
                }
                respawn.PointBList.Add(col.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Ingredient" || col.tag == "Utensil")
        {
            if(role == SpawnRole.SpawnA)
            {
                respawn.PointAList.Remove(col.gameObject);
            }
            else
            {
                respawn.PointBList.Remove(col.gameObject);
            }
            respawn.SpawnItems(role);
        }
    }
}
