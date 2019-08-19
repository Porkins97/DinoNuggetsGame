using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beh : MonoBehaviour
{
Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.0f;
        if(Input.GetKey(KeyCode.W))
        {
            speed = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            speed = 4.0f;
        }



        anim.SetFloat("WalkSpeed", speed);

    }
}
