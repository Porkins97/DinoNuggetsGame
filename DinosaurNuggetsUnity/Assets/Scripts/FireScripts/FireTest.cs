using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireTest : MonoBehaviour
{
    Vector3 position;
    public bool Automatic; // automatically ignite set number of cubes (there is a chance that they are the same, need to update)(numberOfFlames)
    public bool GenISpawn; //changes how it generates, either randomly or via "growth"
    public GameObject prefab;
    public GameObject Empty;
    public int numberOfFlames; // the number of flames automatically ignited on start

    int numSquare = 0;
    public float height;
    public GameObject[,] neighbours;
    public GameObject flame;
    public int xsize, ysize;
    public float flameSize;

    private void Start()
    {
        if(numberOfFlames<1)
        {
            numberOfFlames = 1;
            Debug.Log("Number of flames must be at least 1");
            Debug.Log("Number of flames has been reset to " + numberOfFlames);
        }
        GenerateFire();
    }

    public void Reset(int x, int z)
    {
        neighbours = new GameObject[x,z];
        for (int i = 0, n = 0; i < x; i++)
        {
            for(int j = 0; j< z; j++)
            {
                Vector3 position = new Vector3(i * flameSize, height * flameSize, j * flameSize);
                neighbours[i,j] = Instantiate(Empty, position, Quaternion.identity) as GameObject;
                flame = neighbours[i, j];
                flame.transform.parent = this.gameObject.transform;
                flame.name = ("Flame." + n);
                flame.GetComponent<Flame>().SetNeighboursArray(ref neighbours, i, j, x, z);
                n++;
            }
        }
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                neighbours[i,j].GetComponent<Flame>().GetNeighbours();
            }
        }
    }

    public void GenerateFire()
    {
        numSquare = xsize * ysize;
        Reset(xsize, ysize);

        while(numberOfFlames > numSquare)
        {
            Debug.Log("Error: number of flames must be less than max squares");
            numberOfFlames = numberOfFlames / 10;
            Debug.Log("Number of flames has been reset to " + numberOfFlames);
        }

        if (Automatic == true)
        {
            for(int i = 0; i<numberOfFlames; i++)
            {
                neighbours[Random.Range(0, xsize), Random.Range(0, ysize)].GetComponent<Flame>().Ignite();
            }
        }
    }
}
