using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnIngredient : MonoBehaviour
{
    public GameObject Ingredient;
    private GameObject ThisGameObject;
    public GameObject NewIngredient;
    private Transform ThisPosition;
    private Transform iRotation;
    private float Timer = 3.5f;
    float ResetTime = 1f;
    Collider col;
    public bool ResetBool = true;
    bool dontSpawn = false;
    bool collidingWithIngredient = false;
    bool waitingToSpawn = false;
    public IngredientType iType;
    public DinoSceneManager dinoSceneManager;

    void Start()
    {
        dinoSceneManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DinoSceneManager>();//FindObjectOfType<GameManager>().GetComponent<DinoSceneManager>();
        ThisGameObject = this.gameObject;
        ThisPosition = ThisGameObject.transform;
        col = GetComponent<Collider>();
        StartCoroutine(Reset());
    }

    private void Spawn()
    {
        //add check here "if colliding with other ingredients
        Collider[] colliders = Physics.OverlapSphere(ThisGameObject.transform.position, 0.1f);
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
            NewIngredient = Instantiate(Ingredient, ThisPosition);
            NewIngredient.transform.position = ThisGameObject.transform.position;
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
                Debug.Log("Stage0");
                iRotation = other.gameObject.transform;
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                iType = other.gameObject.GetComponent<ItemAttributes>().GameType;
                Debug.Log("Stage1");
                if(other.gameObject.tag == "Ingredient")
                {
                    Debug.Log("Stage2a");
                    SO_Ingredients foundIngredient = dinoSceneManager.ingredientList.Find(x => x.type == iType);
                    Ingredient = foundIngredient.ingredientPrefab;
                }
                else{
                    Debug.Log("Stage2b");
                    SO_Utensils foundUtensil = dinoSceneManager.utensilList.Find(x => x.type == iType);
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
                StartCoroutine(Respawn());
            dontSpawn = true;
        }
        else
        {
            if((other.gameObject.tag == "Ingredient" || other.gameObject.tag == "Utensil") && waitingToSpawn)
            {
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
