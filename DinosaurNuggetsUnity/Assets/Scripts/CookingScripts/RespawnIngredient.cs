﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnIngredient : MonoBehaviour
{
    public GameObject Ingredient;
    public GameObject NewIngredient;
    public Transform iRotation;
    public Vector3 iPosition;
    public Transform iParent;
    private float Timer = 1.5f;
    float ResetTime = 1f;
    Collider col;
    public bool ResetBool = true;
    public bool dontSpawn = false;
    public bool collidingWithIngredient = false;
    public bool waitingToSpawn = false;
    public IngredientType iType;
    public DinoSceneManager dinoSceneManager;
    Collider[] colliders;
    

    void Start()
    {
        dinoSceneManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DinoSceneManager>();//FindObjectOfType<GameManager>().GetComponent<DinoSceneManager>();
        col = GetComponent<Collider>();
        StartCoroutine(Reset());
        InvokeRepeating("CheckForCollision", 2.0f, 5.0f);
    }

    private void Spawn()
    {
        //Debug.Log("Spawning");
        //add check here "if colliding with other ingredients
        colliders = Physics.OverlapSphere(this.gameObject.transform.position, 0.1f);
        //Debug.Log(colliders.Length);
        for(int i = 0; i <colliders.Length; i++)
        {
            if(colliders[i].gameObject.tag == "Ingredient" || colliders[i].gameObject.tag == "Utensil")
            {
                //Debug.Log(colliders[i]);
                collidingWithIngredient = true;
                waitingToSpawn = true;
            }
        }
    
        if(!collidingWithIngredient)
        {
            Quaternion target = Quaternion.Euler(0,0,0);
            NewIngredient = Instantiate(Ingredient, iPosition, target, iParent);          
            //NewIngredient.transform.rotation = target;
            NewIngredient.GetComponent<Collider>().enabled = true;
            NewIngredient.GetComponent<Rigidbody>().useGravity = true;
            NewIngredient.GetComponent<ItemAttributes>().beingUsed = false;
            NewIngredient.transform.localScale = iRotation.transform.localScale;
            collidingWithIngredient = false;
            waitingToSpawn = false;
        } 
        else
        {
            StartCoroutine(Respawn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Ingredient" || other.gameObject.tag == "Utensil") && Ingredient == null)
        {
            if(ResetBool)
            {
                //Debug.Log("Stage0");
                iRotation = other.gameObject.transform;
                iPosition = new Vector3(other.gameObject.transform.position.x,other.gameObject.transform.position.y,other.gameObject.transform.position.z);
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                iType = other.gameObject.GetComponent<ItemAttributes>().GameType;
                //Debug.Log("Stage1");
                if(other.gameObject.tag == "Ingredient")
                {
                    //Debug.Log("Stage2a");
                    SO_Ingredients foundIngredient = dinoSceneManager.ingredientList.Find(x => x.type == iType);
                    //Debug.Log("Found Ingredient = " + foundIngredient);
                    Ingredient = foundIngredient.ingredientPrefab;
                }
                else{
                    //Debug.Log("Stage2b");
                    SO_Utensils foundUtensil = dinoSceneManager.utensilList.Find(x => x.type == iType);
                    //Debug.Log("Found Utensil = " + foundUtensil);
                    Ingredient = foundUtensil.utensilPrefab; 
                }
            }
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit1");
        //Debug.Log(other.gameObject);
        //Debug.Log(Ingredient);
        if (other.gameObject == Ingredient.gameObject)
        {
            //Debug.Log("Exit2");
            if (!dontSpawn)
            {
                //Debug.Log("Exit3");
                StartCoroutine(Respawn());
            }
            dontSpawn = true;
        }
        else
        {
            if ((other.gameObject.tag == "Ingredient" || other.gameObject.tag == "Utensil"))// && waitingToSpawn)
            {
                //Debug.Log("Exit4");
                collidingWithIngredient = false;
                StartCoroutine(Respawn());
            } 
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(ResetTime);
        ResetBool = false;
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(Timer);
        Spawn();
        dontSpawn = false;
    }
    private void CheckForCollision()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 0.4f);
        if(cols.Length <= 1)
        {
            dontSpawn = false;
            Spawn();
        }
    }
}
//only respawns ingredients atm
