using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnIngredient : MonoBehaviour
{
    public GameObject Ingredient;
    private GameObject ThisGameObject;
    public GameObject NewIngredient;
    private Transform ThisPosition;
    private float Timer = 3.5f;
    Collider col;

    void Start()
    {
        ThisGameObject = this.gameObject;
        ThisPosition = ThisGameObject.transform;
        col = GetComponent<Collider>();
    }

    private void Spawn()
    {
        NewIngredient = Instantiate(Ingredient, ThisPosition);
        NewIngredient.transform.SetParent(ThisGameObject.transform);
        NewIngredient.transform.position = ThisGameObject.transform.position;
        NewIngredient.GetComponent<Collider>().enabled = true;
        NewIngredient.GetComponent<Rigidbody>().useGravity = true;
        Ingredient = NewIngredient;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ingredient" && Ingredient == null)
        {
            Ingredient = other.gameObject;
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Ingredient" && other.gameObject == Ingredient)
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(Timer);

        Spawn();
    }
}
//only respawns ingredients atm
