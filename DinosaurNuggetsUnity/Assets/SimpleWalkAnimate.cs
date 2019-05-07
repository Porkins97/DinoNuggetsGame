using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWalkAnimate : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", speed);
    }
}
