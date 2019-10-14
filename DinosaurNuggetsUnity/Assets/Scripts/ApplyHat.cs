using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHat : MonoBehaviour
{
    int hatNum;
    public HatType hat;
    public GameObject[] hats;
    public bool randomHat;

    void Start()
    {
        if (randomHat == true)
        {
            hatNum = Random.Range(0, hats.Length);
            hat = (HatType)hatNum;
        }       
        for(int i = 0; i< hats.Length; i++)
        {
            if (hats[i].GetComponent<DinoCustomise>().hatType == hat)
            {
                GameObject appliedHat = Instantiate(hats[i], this.gameObject.transform);
                appliedHat.transform.parent = this.gameObject.transform;
            }
        }
    }
}
