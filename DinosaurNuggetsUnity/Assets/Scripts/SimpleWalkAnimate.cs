using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWalkAnimate : MonoBehaviour
{
    Animator animator;
    public Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = rb.velocity.magnitude * 100000;

        animator.SetFloat("Speed", speed);
    }
}
