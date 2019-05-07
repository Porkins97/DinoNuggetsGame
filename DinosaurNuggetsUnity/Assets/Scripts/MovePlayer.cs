﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

     Rigidbody rb;
    bool grounded = true;
    public float walkspeed = 6.0F;
    public float turnspeed = 3.0f;
    public float jumpSpeed = 8.0F;
    public int numJumps = 2;
    private int jumpsUsed = 0;

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
        float effectiveSpeed;

        effectiveSpeed = walkspeed;

        rb.velocity = (transform.forward * fwd * effectiveSpeed) + (transform.up * rb.velocity.y);

        transform.Rotate( Vector3.up * trn * turnspeed);


    }
 
}