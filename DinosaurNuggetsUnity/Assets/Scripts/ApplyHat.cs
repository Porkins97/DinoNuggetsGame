using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHat : MonoBehaviour
{
    public int hatNum;
    public HatType hat;
    public GameObject[] hats;
    public bool randomHat;
    public Transform hatLocator;
    GameObject appliedHat;

    private void Start()
    {
        PassHatnum(0);
    }

    public void SelectHat()
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
                appliedHat = Instantiate(hats[i], hatLocator.gameObject.transform);
                appliedHat.transform.parent = hatLocator.gameObject.transform;
            }
        }
    }

    public void PassHatnum(int num)
    {
        hatNum = num;
        if (appliedHat != null)
            Destroy(appliedHat);
        hat = (HatType)hatNum;
        SelectHat();
    }

    public void PassNewHat(HatType _hat)
    {
        if (appliedHat != null)
            Destroy(appliedHat);
        hat = _hat;
        SelectHat();
    }
}
