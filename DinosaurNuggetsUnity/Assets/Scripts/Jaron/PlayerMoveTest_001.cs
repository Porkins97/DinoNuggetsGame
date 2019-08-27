using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTest_001 : MonoBehaviour
{
    private Animator _anim;
    private Transform _player;

    [SerializeField] private float _playerSpeed;


    void Start()
    {
        _player = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        ApplyInput();

        float currentSpeed = 0.0f;

        if (h != 0 || v != 0)
        {
            _anim.SetFloat("animWalk", Mathf.Clamp01(h + v));
        }
        /*
        float speed = 0.0f;
        if (Input.GetKey(KeyCode.W))
        {
            speed = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            speed = 4.0f;
        }
        */
      
    }

    private void ApplyInput()
    {
        Move();
        Turn();
    }
    private void Move()
    {
        
    }
    private void Turn()
    {

    }


}
