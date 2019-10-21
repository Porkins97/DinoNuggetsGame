using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public float maxSize;
    public float growFactor;
    public float waitTime;
    static int numCubes;
    public GameObject gridSpawn;
    bool cantSpawn = false;
    public float flameSize;
    

    void Start()
    {
        flameSize = gridSpawn.GetComponent<FireTest>().flameSize;
        growFactor += Random.Range(-0.1f, .5f);
        //Collider[] colCheck = Physics.OverlapSphere(this.transform.position, 0.4f);
        //Debug.Log(colCheck);
        //if(!cantSpawn)
        //{
            StartCoroutine(Grow());
        //}
    }

    IEnumerator Grow()
    {
        float timer = 0;
        while (maxSize > transform.localScale.y)
        {
            timer += Time.deltaTime;
            transform.localScale += new Vector3(flameSize, flameSize, flameSize) * Time.deltaTime * growFactor;
            yield return null;
        }
        transform.localScale = new Vector3(flameSize, flameSize, flameSize);
        yield return new WaitForSeconds(waitTime);
    }
}
