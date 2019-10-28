using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoRespawn : MonoBehaviour
{
    public Transform spawnParent = null;
    public Collider SpawnPointA = null;
    public Collider SpawnPointB = null;
    public Vector3 IngredA_Pos = new Vector3();
    public Vector3 IngredB_Pos = new Vector3();

    public Quaternion IngredA_Rot = new Quaternion();
    public Quaternion IngredB_Rot = new Quaternion();

    public IngredientType IngredientA;
    public IngredientType IngredientB;
    public DinoSceneManager _DSM;
    public List<GameObject> PointAList = new List<GameObject>();
    public List<GameObject> PointBList = new List<GameObject>();

    public bool initialSpawnA = false;
    public bool initialSpawnB = false;
    private float timer = 4.0f;

    void Start()
    {
        if (_DSM == null)
        {
            _DSM = GameObject.FindGameObjectWithTag("Manager").GetComponent<DinoSceneManager>();
        }
    }

    public void SpawnItems(SpawnRole role)
    {            
        StartCoroutine(Respawn(role));
    }
    
    private void Spawn(SpawnRole role)
    {
        //StopAllCoroutines();
        if(role == SpawnRole.SpawnA)
        {
            SO_Ingredients ingredA = _DSM.ingredientList.Find(x => x.type == IngredientA);
            GameObject NewIngredient = Instantiate(ingredA.ingredientPrefab, IngredA_Pos, IngredA_Rot, spawnParent); 
            Reset(ref NewIngredient);
        }
        if(role == SpawnRole.SpawnB)
        {
            SO_Ingredients ingredB = _DSM.ingredientList.Find(x => x.type == IngredientB);
            GameObject NewIngredient = Instantiate(ingredB.ingredientPrefab, IngredB_Pos, IngredB_Rot, spawnParent);
            Reset(ref NewIngredient);
        }
        //NewIngredient.GetComponent<Collider>().enabled = true;
        //NewIngredient.GetComponent<Rigidbody>().useGravity = true;
        //NewIngredient.GetComponent<ItemAttributes>().beingUsed = false;
       // NewIngredient.transform.localScale = iRotation.transform.localScale;
    }

    public void Reset(ref GameObject obj)
    {
        obj.GetComponent<Collider>().enabled = true;
        obj.GetComponent<Rigidbody>().useGravity = true;
        obj.GetComponent<ItemAttributes>().beingUsed = false;
    }

    public void Populate(GameObject obj)
    {

    }
    private IEnumerator Respawn(SpawnRole role)
    {
        yield return new WaitForSeconds(timer);
        Spawn(role);
        yield return null;
    }
}
