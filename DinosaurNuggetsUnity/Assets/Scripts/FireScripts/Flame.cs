using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flame : MonoBehaviour
{
    int maxX, maxY, x, y;

    public bool OnFire = false;
    public bool[] ya;
    public GameObject[] localNeighbours = new GameObject[4];
    public GameObject gridSpawn;
    private GameObject[,] neighbours;
    public GameObject prefab;
    public float waitTime;
    
    private bool Ignited = false;

    public Text numCubeText;

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
        if (this.gameObject.name == "Flame.433")
        {
            OnFire = true;
        }
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

        /*
         neighbour 1 = (x, y+1)
            if y == max, neighbour 1 doesn't exist
         neighbour 2 = (x, y-1)
            if y = 0, neighbour 2 doesn't exist
         neighbour 3 = (x+1, y)
            if x = max, neighbour 3 doesn't exist
         neighbour 4 = (x-1, y)
            if x = 0, neighbour 4 doesn't exist

         For each neighbour that != null, and is not on fire already, Ingite()
         Call each neighbour Flame() script and check Flame().OnFire = false;
         Change OnFire = true and start the ignition process.
         Random time between 1 and 5 seconds spawn a game object. Once spawned, calle Ignite() on neighbours.
         */
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
                //Collider[] colliders = Physics.OverlapSphere(localNeighbours[i].transform.position, 0.4f);
                //if (colliders.Length == 0)
                //{
                    StartCoroutine(Hold(i));      
                //}
            }
        }
    }

    public IEnumerator Hold(int i)
    {
        yield return new WaitForSeconds(waitTime);
        localNeighbours[i].GetComponent<Flame>().OnFire = true;
    }
}
