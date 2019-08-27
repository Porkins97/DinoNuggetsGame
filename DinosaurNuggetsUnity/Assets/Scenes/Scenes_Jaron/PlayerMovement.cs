using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float turnSpeed = 95.0f;
    [SerializeField] private float speed = 5.0f;
    private Rigidbody _rb;
    private Vector3 _inputs;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        //transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        //Vector3 vel= transform.forward * Input.GetAxis("Vertical") * speed;
        //var controller = GetComponent<CharacterController>();
        //controller.SimpleMove(vel);

        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");

        _inputs = _inputs.normalized;

    }



    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _inputs * speed * Time.fixedDeltaTime);
    }
}
