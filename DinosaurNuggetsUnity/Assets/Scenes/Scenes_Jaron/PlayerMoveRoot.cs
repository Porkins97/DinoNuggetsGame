using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveRoot : MonoBehaviour
{
    
    
    [SerializeField]private bool blockRotationPlayer;
    [SerializeField]private float desiredRotationSpeed;
    
    [SerializeField]private float allowPlayerRotation;

    private float speed;
    private float h;
    private float v;
    private float verticalVel;
    private bool isGrounded;
    private Vector3 desiredMoveDirection;
    private Vector3 moveVector;
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

        //Character Grounded
        isGrounded = controller.isGrounded;
        if(isGrounded)
            verticalVel -= 0.0f;
        else
            verticalVel -= 2.0f;

        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);
    }
    void InputMagnitude()
    {
        //Calulate Input Vectors
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //Calculate Input Magnitude
        speed = new Vector2(h, v).sqrMagnitude;

        //Move the method
        if(speed > allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else
        {
            anim.SetFloat("InputMagnitude", 0.0f, 0.0f, Time.deltaTime);
        }
    }
    void PlayerMoveAndRotation()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        float angle = Mathf.Atan2(h, v);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, Mathf.Rad2Deg*angle, 0.0f), desiredRotationSpeed);
    }

}
