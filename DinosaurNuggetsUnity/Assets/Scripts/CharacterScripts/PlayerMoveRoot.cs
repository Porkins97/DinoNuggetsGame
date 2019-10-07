using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DinoNuggets.CustomInputManager;

public class PlayerMoveRoot : MonoBehaviour
{
    
    [Header("Player Speeds")]
    [SerializeField]private float desiredRotationSpeed = 0.2f;
    [SerializeField]private float allowPlayerRotation = 0.0f;
    [Header("Arm transforms")]
    [SerializeField]private Transform rightArm = null;
    [SerializeField]private Transform leftArm = null;

    private CharacterController _controller = null;
    private Animator _anim = null;
    private CustomInputManager CIM;
    
    void Start()
    {
        _anim = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        CIM = GetComponent<CustomInputManager>();
        Debug.Log(CIM);
    }

    void Update()
    {
        InputMagnitude();
        Ground();
    }

    void LateUpdate()
    {
        if(CIM.dinoArmMove.x > 0.5)
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
        float h = CIM.dinoMove.x;
        float v = CIM.dinoMove.y;

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
