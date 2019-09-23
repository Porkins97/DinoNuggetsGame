using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public Transform stoveTopA_Loc = null;
    public bool stoveTopA_Used = false;
    private GameObject stoveAGameObject = null;

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
}
