using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveRoot : MonoBehaviour
{
    [SerializeField]private float desiredRotationSpeed = 0.2f;
    [SerializeField]private float allowPlayerRotation = 0.0f;
    [SerializeField]private Transform rightArm;
    [SerializeField]private Transform leftArm;

    private CharacterController _controller;
    private Animator _anim;
    
    private PlayerControls _controls;
    private Vector2 _move;
    private Vector2 _armMove;

    void Awake()
    {
        _controls = new PlayerControls();
        //_controls.Gameplay.Grow.performed += ctx => Grow();
        _controls.Gameplay.Move.performed += ctx => _move = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Move.canceled += ctx => _move = Vector2.zero;

        _controls.Gameplay.Arms.performed += ctx => _armMove = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Arms.canceled += ctx => _armMove = Vector2.zero;
    }
    void OnEnable()
    {
        _controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        _controls.Gameplay.Disable();
    }
    void Start()
    {
        _anim = this.GetComponent<Animator>();
        _controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        InputMagnitude();
        Ground();
    }

    void LateUpdate()
    {
        if(_armMove.x > 0.5)
        {
            rightArm.rotation = Quaternion.Euler(80f, rightArm.rotation.y, rightArm.rotation.z);
        }
    }

    private void Ground()
    {
        bool isGrounded = _controller.isGrounded;
        float verticalVel = 0.0f;
        if(isGrounded)
            verticalVel -= 0.0f;
        else
            verticalVel -= 2.0f;
        Vector3 moveVector = new Vector3(0, verticalVel, 0);
        _controller.Move(moveVector);
    }

    void InputMagnitude()
    {
        //Calulate Input Vectors
        float h = _move.x;
        float v = _move.y;

        //Calculate Input Magnitude
        float speed = new Vector2(h, v).sqrMagnitude;

        //Move the method
        if(speed > allowPlayerRotation)
        {
            _anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
            PlayerMoveAndRotation(h, v);
        }
        else
        {
            _anim.SetFloat("InputMagnitude", 0.0f, 0.0f, Time.deltaTime);
        }
    }

    void PlayerMoveAndRotation(float h, float v)
    {
        float angle = Mathf.Atan2(h, v);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, Mathf.Rad2Deg*angle, 0.0f), desiredRotationSpeed);
    }

}
