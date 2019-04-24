using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    public float rotatespeed;
  
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Moves character along one axis
        transform.Translate(0f, 0f, speed * Input.GetAxis("Vertical") * Time.fixedDeltaTime);

        transform.Rotate(0f, rotatespeed*Input.GetAxis("Horizontal") * Time.fixedDeltaTime, 0f, Space.Self);
    }
}
