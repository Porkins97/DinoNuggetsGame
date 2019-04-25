using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    public float rotatespeed;
    public GameObject Dino;
    private Rigidbody ThisRigidBody = null;

    // Start is called before the first frame update
    void Start()
    {
        ThisRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Moves character along one axis
        transform.Translate(0f, 0f, speed * Input.GetAxis("Vertical") * Time.fixedDeltaTime);

        transform.Rotate(0f, rotatespeed*Input.GetAxis("Horizontal") * Time.fixedDeltaTime, 0f, Space.Self);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Dino.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
