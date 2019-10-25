using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(PickupScript)), RequireComponent(typeof(CharacterController)), RequireComponent(typeof(Animator))]
public class PlayerMoveRoot : MonoBehaviour
{
    
    [Header("Player Speeds")]
    [SerializeField]private float desiredRotationSpeed = 0.2f;
    [SerializeField]private float allowPlayerRotation = 0.0f;
    [Header("Arm transforms")]
    [SerializeField]private Transform rightArm = null;
    [SerializeField]private Transform leftArm = null;

    [Header("User Settings")]
    [SerializeField] private int gamePadId = 0;
    [SerializeField] private InputSystemUIInputModule uiInput = null;
    [SerializeField] private DinoSceneManager _sceneManager = null;
    [SerializeField] private UnityEvent PauseEvent = null;

    //Private Variables
    private CharacterController _controller = null;
    private Animator _anim = null;

    private InputActionMap _actions;
    private InputActionMap _UI;

    private Vector2 dinoMove;
    private Vector2 dinoArmMove;
    public bool dinoRightHand;
    public bool dinoLeftHand;

    void Awake()
    {
        InputActionAsset input = Instantiate(uiInput.actionsAsset);

        _actions = input.FindActionMap("Player");
        _UI = input.FindActionMap("UI");

        InputUser currentUser = InputUser.PerformPairingWithDevice(Gamepad.all[gamePadId]);
        currentUser.AssociateActionsWithUser(input);
        
        _sceneManager.allUsers.Add(new UserInputs(currentUser, _UI, _actions, input));


        InputAction movement = _actions.FindAction("Movement");
        InputAction leftHandGrab = _actions.FindAction("LeftHandGrab");
        InputAction rightHandGrab = _actions.FindAction("RightHandGrab");
        InputAction pause = _actions.FindAction("Pause Menu");

        InputAction unPause = _UI.FindAction("Pause Menu");
        
        movement.performed += ctx => dinoMove = ctx.ReadValue<Vector2>();
        movement.canceled += ctx => dinoMove = Vector2.zero;

        leftHandGrab.performed += LeftHand;
        leftHandGrab.canceled += LeftHand;

        rightHandGrab.performed += RightHand;
        rightHandGrab.canceled += RightHand;

        pause.performed += ctx => PauseEvent.Invoke();
        unPause.performed += ctx => PauseEvent.Invoke();
    }
    public void LeftHand(InputAction.CallbackContext ctx)
    {
        float p = ctx.ReadValue<float>();
        if(p > 0)
            dinoLeftHand = true;
        else
            dinoLeftHand = false;
    }
    public void RightHand(InputAction.CallbackContext ctx)
    {
        float p = ctx.ReadValue<float>();
        if(p > 0)
            dinoRightHand = true;
        else
            dinoRightHand = false;
    }

    public void OnEnable()
    {
        _actions.Enable();
        _UI.Disable();
    }

    public void OnDisable()
    {
        _actions.Disable();
        _UI.Disable();
    }
    
    void Start()
    {
        _anim = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        InputMagnitude();
        Ground();
    }

    void LateUpdate()
    {
        if(dinoArmMove.x > 0.5)
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
        float h = dinoMove.x;
        float v = dinoMove.y;

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
