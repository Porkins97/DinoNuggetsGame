using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    public float rotatespeed;
    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Moves character along one axis
        transform.Translate(0f, 0f, speed * Input.GetAxis("Vertical") * Time.deltaTime);

        transform.Rotate(0f, rotatespeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, Space.Self);
    }
}
