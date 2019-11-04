using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flame : MonoBehaviour
{
    //[SerializeField] public IngredientType GameType;
    int maxX, maxY, x, y;
    public bool OnFire = false;
    public GameObject[] localNeighbours = new GameObject[4];
    public GameObject gridSpawn;
    private GameObject[,] neighbours;
    public GameObject prefab;
    public float waitTime;
    IngredientType cuttingBoard = IngredientType.Cutting_Board;
    IngredientType fireExtinguisher = IngredientType.Fire_Extinguisher;
    GameObject Fire;
    void Reset()
    {
        for (int i = 0; i<localNeighbours.Length; i++)
        {
            localNeighbours[i] = null;
        }
    }

    void Start() 
    {
        waitTime = waitTime + Random.Range(-2,2);
        //Vector3 newPos  = this.gameObject.transform.position;
        //this.gameObject.transform.position = newPos + Vector3.up * 0.4f;
    }

    public void SetNeighboursArray(ref GameObject[,] n, int i, int j, int xm, int ym)
    {
        neighbours = n;
        x = i;
        y = j;
        maxX = xm-1;
        maxY = ym-1;
    }

    public void GetNeighbours()
    {
        Reset();

        if(y != maxY)
        {
            localNeighbours[0] = neighbours[x, y + 1];
        }
        if(y != 0)
        {
            localNeighbours[1] = neighbours[x, y - 1];
        }
        if(x != maxX)
        {
            localNeighbours[2] = neighbours[x + 1, y];
        }
        if (x != 0)
        {
            localNeighbours[3] = neighbours[x - 1, y];
        }
    }
    public void Ignite()
    {
        OnFire = true;
        if(Fire == null)
        {
            Fire = Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
            Fire.transform.parent = this.gameObject.transform;
        }
        else
        {
            Fire.SetActive(true);
        }
        Spread();
    }

    public IEnumerator Hold(int i)
    {
        yield return new WaitForSeconds(waitTime);
        localNeighbours[i].GetComponent<Flame>().Ignite();
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.5f);
        Ignite();
    }

    public IEnumerator Extinguish()
    {
        yield return new WaitForSeconds(0.15f);
        Fire.SetActive(false);
        StopAllCoroutines();
    }
    public IEnumerator Check()
    {
        yield return new WaitForSeconds(5f);
        if(OnFire)
        {
            Spread();
        }
    }
    void Spread()
    {
        for (int i = 0; i<localNeighbours.Length; i++)
        {
            if (localNeighbours[i] != false && localNeighbours[i].GetComponent<Flame>().OnFire == false)
            {
                    StartCoroutine(Hold(i));      
            }
            else
            {
                //StartCoroutine(Check());
            }
        }
    }

    private void OnTriggerEnter(Collider other)  //update this to check how long it was in the area
                                                 //if it has been area for x.time, Ignite(), rather than ignite no matter how long
    {
        if(other.gameObject.tag == "Utensil")
        {
            if(other.gameObject.GetComponent<ItemAttributes>().GameType == cuttingBoard)
            {
                if(other.gameObject.transform.childCount >2)
                {   
                    StartCoroutine(Wait());
                }
            }
            if(other.gameObject.GetComponent<ItemAttributes>().GameType == fireExtinguisher);
            {
                if(other.gameObject.GetComponent<ItemAttributes>().beingUsed == true && transform.childCount > 0)
                {
                    OnFire = false;
                    StartCoroutine(Extinguish());
                }
            }
        }
        if(other.gameObject.tag == "Cupboard")
        {
            Debug.Log("Moving");
            Vector3 newPos  = this.gameObject.transform.position;
            this.gameObject.transform.position = newPos + Vector3.up * 0.4f;
        }
    }
}
