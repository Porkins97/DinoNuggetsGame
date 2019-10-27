using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnIngredient : MonoBehaviour
{
    public GameObject Ingredient;
    public GameObject NewIngredient;
    private Transform ThisPosition;
    private Transform iRotation;
    private float Timer = 3.5f;
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
    }

    private void Spawn()
    {
        Debug.Log("Spawning");
        //add check here "if colliding with other ingredients
        colliders = Physics.OverlapSphere(this.gameObject.transform.position, 0.1f);
        Debug.Log(colliders.Length);
        for(int i = 0; i <colliders.Length; i++)
        {
            if(colliders[i].gameObject.tag == "Ingredient" || colliders[i].gameObject.tag == "Utensil")
            {
                Debug.Log(colliders[i]);
                collidingWithIngredient = true;
                waitingToSpawn = true;
            }
        }
    
        if(!collidingWithIngredient)
        {
            NewIngredient = Instantiate(Ingredient, this.gameObject.transform);
            NewIngredient.transform.rotation = iRotation.transform.rotation;
            NewIngredient.GetComponent<Collider>().enabled = true;
            NewIngredient.GetComponent<Rigidbody>().useGravity = true;
            NewIngredient.GetComponent<ItemAttributes>().beingUsed = false;
            NewIngredient.transform.localScale = new Vector3(1f, 1f, 1f);
            collidingWithIngredient = false;
            waitingToSpawn = false;
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
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                iType = other.gameObject.GetComponent<ItemAttributes>().GameType;
                //Debug.Log("Stage1");
                if(other.gameObject.tag == "Ingredient")
                {
                    //Debug.Log("Stage2a");
                    SO_Ingredients foundIngredient = dinoSceneManager.ingredientList.Find(x => x.type == iType);
                    Debug.Log("Found Ingredient = " + foundIngredient);
                    Ingredient = foundIngredient.ingredientPrefab;
                }
                else{
                    //Debug.Log("Stage2b");
                    SO_Utensils foundUtensil = dinoSceneManager.utensilList.Find(x => x.type == iType);
                    Debug.Log("Found Utensil = " + foundUtensil);
                    Ingredient = foundUtensil.utensilPrefab; 
                }
            }
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == Ingredient)
        {
            if (!dontSpawn)
            {
                StartCoroutine(Respawn());
            }
            dontSpawn = true;
        }
        else
        {
            Debug.Log("EEAS");
            if ((other.gameObject.tag == "Ingredient" || other.gameObject.tag == "Utensil") /*&& waitingToSpawn*/)
            {
                Debug.Log("ABC");
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
}
//only respawns ingredients atm
