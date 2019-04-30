using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCupboard : MonoBehaviour
{
    public static bool Cut = false;

    public GameObject Knife;
    public GameObject Cupboard;

    private Rigidbody ThisRigidBody;
    private Collider ThisCollider;

    // Start is called before the first frame update
    void Start()
    {
        Knife = GameObject.Find("Knife_001");
        Cupboard = this.gameObject;
        ThisRigidBody = GetComponent<Rigidbody>();
        ThisCollider = GetComponent<Collider>();
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
