﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    Rigidbody rb;
    public float walkspeed = 6.0F;
    public float turnspeed = 3.0f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float fwd = Input.GetAxis("Vertical");
        float trn = Input.GetAxis("Horizontal"); 

        rb.velocity = (transform.forward * fwd * walkspeed) + (transform.up * rb.velocity.y);

        transform.Rotate( Vector3.up * trn * turnspeed);


    }
 
}