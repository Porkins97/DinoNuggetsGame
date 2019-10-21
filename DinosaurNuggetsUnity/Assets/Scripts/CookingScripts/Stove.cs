using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    [SerializeField] private DinoSceneManager sManager;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private Transform stoveTopA_Loc = null;
    
    [HideInInspector]
    public bool stoveTopA_Used = false;
    private GameObject stoveAGameObject = null;

    public void Start()
    {
        if(sManager == null)
            sManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DinoSceneManager>();
    }

    public void Placed(GameObject objectPlaced)
    {
        if (objectPlaced != null && stoveTopA_Used == false)
        {
            stoveAGameObject = objectPlaced;
            objectPlaced.GetComponent<Rigidbody>().isKinematic = true;
            objectPlaced.GetComponent<BeingUsed>().beingUsed = true;
            objectPlaced.GetComponent<BeingUsed>().onStove = true;
            objectPlaced.transform.position = stoveTopA_Loc.position;
            objectPlaced.transform.rotation = Quaternion.identity;
            stoveTopA_Used = true;
        }
    }
    public void Removed(GameObject objectPlaced)
    {
        if(objectPlaced == stoveAGameObject)
        {
            stoveAGameObject.GetComponent<BeingUsed>().onStove = false;
            stoveAGameObject = null;
            stoveTopA_Used = false;
        }
    }

    public void Burn(GameObject objectPlaced)
    {
        if(objectPlaced.GetComponent<BeingUsed>().Burnable == true)
        {
            IEnumerator coroutine = Burning(2.0f, objectPlaced);
            Placed(objectPlaced);
            objectPlaced.GetComponent<BeingUsed>().Locked = true;
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator Burning(float waitTime, GameObject destroyObj)
    {
        GameObject fire = Instantiate(firePrefab, destroyObj.transform);
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Removed(destroyObj);
            Destroy(fire);
            Destroy(destroyObj);
        }
    }
}
