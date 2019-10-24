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
    private bool Ignited = false;
    IngredientType cuttingBoard = IngredientType.Cutting_Board;
    IngredientType fireExtinguisher = IngredientType.Fire_Extinguisher;
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
        /*if (this.gameObject.name == "Flame.433")
        {
            OnFire = true;
        }*/
    }

    void FixedUpdate()
    {
        if(OnFire == true && Ignited == false)
        {
            Ignite();
        }
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
        Ignited = true;
        GameObject Cube = Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
        Cube.transform.parent = this.gameObject.transform;
        

        for (int i = 0; i<localNeighbours.Length; i++)
        {
            if (localNeighbours[i] != false && localNeighbours[i].GetComponent<Flame>().OnFire == false)
            {
                    StartCoroutine(Hold(i));      
            }
        }
    }

    public IEnumerator Hold(int i)
    {
        yield return new WaitForSeconds(waitTime);
        localNeighbours[i].GetComponent<Flame>().OnFire = true;
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        Ignite();
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
            if(other.gameObject.GetComponent<ItemAttributes>().GameType == fireExtinguisher)
            {
                if(this.gameObject.transform.childCount > 0)
                {
                    OnFire = false;
                }
            }
        }
    }
}
