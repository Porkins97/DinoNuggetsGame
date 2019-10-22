using UnityEngine;
using DinoInputSystems;

[RequireComponent(typeof(DinoInputSystem)), RequireComponent(typeof(Animator)), RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //Serialized fields
    [Header("Player Speeds")]
    [SerializeField] private float desiredRotationSpeed = 0.2f;
    [SerializeField] private float allowPlayerRotation = 0.0f;
    [Header("Arm transforms")]
    [SerializeField] private Transform rightArm = null;
    [SerializeField] private Transform leftArm = null;

    //Private Variables
    private Animator _anim = null;
    private CharacterController _controller = null;
    private DinoInputSystem _DIS = null;

    //Inherited variables
    private Vector2 dinoMove { get { return _DIS.dinoMove; } }
    private Vector2 dinoArmMove { get { return _DIS.dinoArmMove; } }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _DIS = GetComponent<DinoInputSystem>();
    }

    void Update()
    {
        InputMagnitude();
        Ground();
    }

    private void Ground()
    {
        bool isGrounded = _controller.isGrounded;
        float verticalVel = 0.0f;
        if (isGrounded)
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
        if (speed > allowPlayerRotation)
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, Mathf.Rad2Deg * angle, 0.0f), desiredRotationSpeed);
    }

}
