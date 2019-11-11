using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    // Serializes variables
    [SerializeField] private GameObject firePrefab = null;
    [SerializeField] private Transform fireLocation = null;
    [SerializeField] private Transform stoveTopA_Loc = null;
    [SerializeField] private bool OutDoorStove = false;


    // Public variables
    [SerializeField] public bool stoveTopA_Used = false;
    [SerializeField] public GameObject stoveAGameObject = null;

    // Private variables
    private GameObject fireInstance = null;
    private DinoSceneManager _DSM = null;

    public void Start()
    {
        if (_DSM == null)
        {
            _DSM = GameObject.FindGameObjectWithTag("Manager").GetComponent<DinoSceneManager>();
        }
    }

    public void Placed(GameObject objectPlaced)
    {
        if (objectPlaced != null && stoveTopA_Used == false)
        {
            stoveAGameObject = objectPlaced;
            objectPlaced.GetComponent<Rigidbody>().isKinematic = true;
            objectPlaced.GetComponent<ItemAttributes>().onStove = true;
            objectPlaced.GetComponent<PotCooking>().enabled = true;
            objectPlaced.GetComponent<PotCooking>().currentCol.enabled = true;
            if (objectPlaced.GetComponent<ItemAttributes>().wasLocked == true)
            {
                objectPlaced.GetComponent<ItemAttributes>().Locked = true;
            }
            objectPlaced.transform.position = stoveTopA_Loc.position;
            objectPlaced.transform.rotation = Quaternion.identity;
            stoveTopA_Used = true;
            if (fireInstance == null && OutDoorStove == true)
            {
                fireInstance = Instantiate(firePrefab, fireLocation.position, fireLocation.rotation, fireLocation);
            }
        }
    }
    public void Removed(GameObject objectPlaced)
    {
        Debug.Log("Removed");
        if(objectPlaced == stoveAGameObject)
        {
            stoveAGameObject.GetComponent<ItemAttributes>().onStove = false;
            objectPlaced.GetComponent<PotCooking>().enabled = false;
            objectPlaced.GetComponent<BoxCollider>().enabled = false;
            stoveAGameObject = null;
            stoveTopA_Used = false;
            if (fireInstance != null)
            {
                Destroy(fireInstance);
                fireInstance = null;
            }
        }
        
    }

    public void Burn(GameObject objectPlaced)
    {
        if (objectPlaced.GetComponent<ItemAttributes>().Burnable == true)
        {
            if(objectPlaced.GetComponent<ItemAttributes>().currentlyBurning == false)
            {
                objectPlaced.GetComponent<ItemAttributes>().currentlyBurning = true;
                IEnumerator coroutine = Burning(4.0f, objectPlaced);
                Placed(objectPlaced);
                objectPlaced.GetComponent<ItemAttributes>().Locked = true;
                StartCoroutine(coroutine);
            }
            
        }
    }

    private IEnumerator Burning(float waitTime, GameObject destroyObj)
    {

        GameObject fire = Instantiate(firePrefab, destroyObj.transform);
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            
            Destroy(fire);
            Destroy(destroyObj);
            stoveAGameObject = null;
            stoveTopA_Used = false;
            yield return null;
        }
    }
}
