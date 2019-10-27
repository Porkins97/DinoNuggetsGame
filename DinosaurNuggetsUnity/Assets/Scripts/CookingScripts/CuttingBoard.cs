using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    [SerializeField] private Transform cuttingBoardLocation = null;
    [SerializeField] public GameObject cuttingCloudPrefab = null;
    [HideInInspector] private GameObject cuttingBoardObject = null;
    [HideInInspector] public bool cuttingBoardUsed = false;

    private DinoSceneManager sManager = null;
    private ItemAttributes attribs = null;

    void Start()
    {
        if (sManager == null)
        {
            sManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DinoSceneManager>();
        }
        if(attribs == null)
        {
            attribs = GetComponent<ItemAttributes>();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ingredient")
        {
            if (col.GetComponent<ItemAttributes>().beingUsed == false)
            {
                Placed(col.gameObject);
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Ingredient")
        {
            if (col.GetComponent<ItemAttributes>().beingUsed == false)
            {
                Placed(col.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ingredient")
        {
            if (col.GetComponent<ItemAttributes>().beingUsed == true)
            {
                Removed(col.gameObject);
            }
        }
    }

    public void Placed(GameObject objectPlaced)
    {
        if (objectPlaced != null && cuttingBoardUsed == false)
        {
            cuttingBoardObject = objectPlaced;
            objectPlaced.GetComponent<Rigidbody>().isKinematic = true;
            objectPlaced.GetComponent<ItemAttributes>().onCuttingBoard = true;
            objectPlaced.GetComponent<ItemAttributes>().currentCuttingBoard = transform;
            objectPlaced.transform.position = cuttingBoardLocation.position;
            objectPlaced.transform.rotation = cuttingBoardLocation.rotation;
            
            cuttingBoardUsed = true;
        }
    }


    public void Removed(GameObject objectPlaced)
    {
        if (objectPlaced == cuttingBoardObject)
        {
            cuttingBoardObject.GetComponent<ItemAttributes>().onCuttingBoard = false;
            cuttingBoardObject = null;
            cuttingBoardUsed = false;
        }

    }
}
