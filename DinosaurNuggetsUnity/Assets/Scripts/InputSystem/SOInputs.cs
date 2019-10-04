using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Keybindings", menuName = "KeyBindings")]
public class SOInputs : ScriptableObject
{
    //public KeyCode Movement;
    [Range(1, 10)]public int joyStick = 1;
    
    [SerializeField]public string currentJoyStick  
    {
        get
        {
            return System.String.Format("joystick {0}", joyStick);
        }
        set{}
    }

}
