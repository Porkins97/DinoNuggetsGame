using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public GameObject Ingredient;
    public GameObject Pot;
    public GameObject Burner;
    
    // Start is called before the first frame update
    void Start()
    {
        Pot = this.gameObject;
        Burner = this.gameObject.transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.parent.gameObject != null)
        {
            Burner = this.gameObject.transform.parent.gameObject;
        }
        else
        {
            Burner = null;
        }


    }
}
