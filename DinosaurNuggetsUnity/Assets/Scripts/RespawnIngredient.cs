using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnIngredient : MonoBehaviour
{
    public GameObject Ingredient;
    private GameObject ThisGameObject;
    public GameObject NewIngredient;
    private Transform ThisPosition;
    float x;
    float y;
    float z;
    public float Timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        ThisGameObject = this.gameObject;
        if (ThisGameObject.transform.childCount > 0)
        {
            Ingredient = ThisGameObject.transform.GetChild(0).gameObject;
            Debug.Log("ChildObject = " + Ingredient);
            ThisPosition = this.gameObject.transform;
            x = Ingredient.transform.localScale.x;
            y = Ingredient.transform.localScale.y;
            z = Ingredient.transform.localScale.z;
        }
        else
            Ingredient = null;
    }

    // Update is called once per frame
    void Update()
    {
        if((Ingredient != null) && (this.gameObject.transform.childCount == 0))
        {
            Timer += Time.deltaTime;

            //Debug.Log("Timer = " + Timer);
            //NewIngredient = Ingredient.gameObject;

            if((Timer>4) && (Ingredient.GetComponent<PickupTest>().ThisItemIsBeingCarried == false))
            {
                Timer = 0f;
                NewIngredient = Instantiate(Ingredient,ThisPosition);
                NewIngredient.transform.SetParent(ThisGameObject.transform);
                NewIngredient.transform.position = ThisGameObject.transform.position;
                NewIngredient.transform.localScale = new Vector3(x, y, z);
                NewIngredient.GetComponent<Collider>().enabled = true;
                NewIngredient.GetComponent<Rigidbody>().useGravity = true;
                Ingredient = NewIngredient.gameObject;
            }
            if(Timer>10)
            {
                Timer = 0;
            }
        }
    }
}
