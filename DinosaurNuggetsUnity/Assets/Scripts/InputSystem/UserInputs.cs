using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.UI;

public class UserInputs
{
    public InputUser currentUser;
    public InputActionMap current_UI;
    public InputActionMap current_Actions;


    public UserInputs(InputUser _currentUser, InputActionMap _current_UI, InputActionMap _current_Actions)
    {
        this.currentUser = _currentUser;
        this.current_UI = _current_UI;
        this.current_Actions = _current_Actions;
    }
}