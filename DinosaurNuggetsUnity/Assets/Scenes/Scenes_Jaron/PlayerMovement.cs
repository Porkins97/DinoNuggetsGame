using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float turnSpeed = 95.0f;
    [SerializeField] private float speed = 5.0f;
    private Rigidbody _rb;
    private Vector3 _inputs;


    private PlayerControls controls;
    private Vector2 move;
    private Vector2 rotate;




    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Grow.performed += ctx => Grow();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Move.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => rotate = Vector2.zero;
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Grow()
    {
        transform.localScale *= 1.1f;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }



    void Update()
    {
        //transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        //Vector3 vel= transform.forward * Input.GetAxis("Vertical") * speed;
        //var controller = GetComponent<CharacterController>();
        //controller.SimpleMove(vel);
/*
      

        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");

        _inputs = _inputs.normalized;
 */

        Vector3 m = new Vector3(move.x, 0.0f, move.y) * speed * Time.deltaTime;
        transform.Translate(m, Space.World);

        Vector3 r = new Vector3(rotate.y, rotate.x, 0.0f) * turnSpeed * Time.deltaTime;
        transform.Rotate(r, Space.World);
    }



    void FixedUpdate()
    {
        //_rb.MovePosition(_rb.position + _inputs * speed * Time.fixedDeltaTime);
    }
}
