using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.UI;

namespace DinoInputSystems
{
    public class DinoInputSystem : MonoBehaviour
    {
        //Serialized variables
        [Header("User Settings")]
        [SerializeField] private int gamePadId = 0;
        [SerializeField] private InputSystemUIInputModule uiInput = null;
        [SerializeField] private DinoSceneManager _sceneManager = null;
        [SerializeField] private UnityEvent PauseEvent = null;
        
        //Private variables
        private InputActionMap _actions;
        private InputActionMap _UI;

        //Shared variables
        internal Vector2 dinoMove;
        internal Vector2 dinoArmMove;
        internal bool dinoRightHand;
        internal bool dinoLeftHand;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Required for input system
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Awake()
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


            pause.performed += ctx => Pause(gamePadId);
            unPause.performed += ctx => Pause(gamePadId);

            pause.performed += ctx => { _sceneManager.userPaused = gamePadId; PauseEvent.Invoke(); };
            unPause.performed += ctx => { _sceneManager.userPaused = gamePadId; PauseEvent.Invoke(); };
        }
        
        private void OnEnable()
        {
            _actions.Enable();
            _UI.Disable();
        }

        private void OnDisable()
        {
            _actions.Disable();
            _UI.Disable();
        }

        private void Pause(int currentUser)
        {
            _sceneManager.userPaused = currentUser;
            PauseEvent.Invoke();
        }

        private void LeftHand(InputAction.CallbackContext ctx)
        {
            float p = ctx.ReadValue<float>();
            if (p > 0)
                dinoLeftHand = true;
            else
                dinoLeftHand = false;
        }

        private void RightHand(InputAction.CallbackContext ctx)
        {
            float p = ctx.ReadValue<float>();
            if (p > 0)
                dinoRightHand = true;
            else
                dinoRightHand = false;
        }

    }
}
