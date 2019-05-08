using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCupboard : MonoBehaviour
{
    public static bool Cut = false;
    public bool Spot1 = false;
    public bool Spot2 = false;

    public GameObject Knife;
    public GameObject Cupboard;
    public GameObject Cuttingspot1;
    public GameObject Cuttingspot2;

    private Rigidbody ThisRigidBody;
    private Collider ThisCollider;

    // Start is called before the first frame update
    void Start()
    {
        Knife = GameObject.Find("Knife_001");
        Cupboard = this.gameObject;
        ThisRigidBody = GetComponent<Rigidbody>();
        ThisCollider = GetComponent<Collider>();
        Cuttingspot1 = this.gameObject.transform.Find("CuttingSpot").gameObject;
        Cuttingspot2 = this.gameObject.transform.Find("CuttingSpot (1)").gameObject;

    }

    private void Update()
    {
        if(Cuttingspot1.transform.childCount == 1)
        {
            Spot1 = true;
        }

        if(Cuttingspot2.transform.childCount == 1)
        {
            Spot2 = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Knife_001")
        {
            Cut = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "Knife_001")
        {
            Cut = false;
        }
    }
}
