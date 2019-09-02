using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveRoot : MonoBehaviour
{
    [SerializeField]private float desiredRotationSpeed;
    [SerializeField]private float allowPlayerRotation;

    private CharacterController controller;
    private Animator anim;
    
    void Start()
    {
        anim = this.GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        InputMagnitude();
        Ground();
    }

    private void Ground()
    {
        bool isGrounded = controller.isGrounded;
        float verticalVel = 0.0f;
        if(isGrounded)
            verticalVel -= 0.0f;
        else
            verticalVel -= 2.0f;
        Vector3 moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);
    }

    void InputMagnitude()
    {
        //Calulate Input Vectors
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Calculate Input Magnitude
        float speed = new Vector2(h, v).sqrMagnitude;

        //Move the method
        if(speed > allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
            PlayerMoveAndRotation(h, v);
        }
        else
        {
            anim.SetFloat("InputMagnitude", 0.0f, 0.0f, Time.deltaTime);
        }
    }
    
    void PlayerMoveAndRotation(float h, float v)
    {
        float angle = Mathf.Atan2(h, v);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, Mathf.Rad2Deg*angle, 0.0f), desiredRotationSpeed);
    }

}
