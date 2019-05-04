using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStaticVariables : MonoBehaviour
{
    public static bool GlobalHeldLeft;
    public static bool GlobalHeldRight;
    public static bool OvenInUse;
    public static bool PickUpOven;

    public bool L_GlobalHeldLeft = false;
    public bool L_GLobalHeldRight = false;
    public bool L_OvenInUse = false;
    public bool L_PickUpOven = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        L_GlobalHeldLeft = GlobalHeldLeft;
        L_GLobalHeldRight = GlobalHeldRight;
        L_OvenInUse = OvenInUse;
        L_PickUpOven = PickUpOven;
    }
}
