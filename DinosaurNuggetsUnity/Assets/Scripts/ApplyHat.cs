using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHat : MonoBehaviour
{
    int hatNum;
    public GameObject hatCheck;
    public HatType hat;
    public GameObject[] hats;
    public bool randomHat;
    // Start is called before the first frame update
    void Start()
    {
        hats = GameObject.FindGameObjectsWithTag("Wearable");
        if (randomHat == true)
        {
            hatNum = Random.Range(0, 12);
            hat = (HatType)hatNum;
            
            for(int i = 0; i< hats.Length; i++)
            {
                if (hats[i].GetComponent<DinoCustomise>().hatType == hat)
                {
                    GameObject appliedHat = Instantiate(hats[i], this.gameObject.transform);
                    appliedHat.transform.parent = this.gameObject.transform;
                    Destroy(hats[i]);
                }
                else
                {
                    Destroy(hats[i]);
                }
            }
        }       
        else
        {
            for(int i =0; i < hats.Length; i++)
            {
                Destroy(hats[i]);
            }
        }
    }
}
