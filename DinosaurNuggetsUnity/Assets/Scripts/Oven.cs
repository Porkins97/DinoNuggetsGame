using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public static bool GlobalHeldLeft;
    public static bool GlobalHeldRight;


    public bool OvenInUse;
    public bool L_GlobalHeldLeft = false;
    public bool L_GlobalHeldRight = false;
    public bool L_OvenInUse = false;
    public bool L_PickUpOven = false;

    private int Pickup = 0;

    public GameObject OvenObject;
    public GameObject Player;
    public GameObject Burner;
    public GameObject Utensil;
    public GameObject HandLeft;
    public GameObject HandRight;
    private float Distance;

    Rigidbody Rb;
    Collider ThisCollider;
    Collider UtensilCollider;
    GameObject UICamera;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Character_Model_01");
        HandLeft = GameObject.Find("Character_Model_01/HandLeft");
        HandRight = GameObject.Find("Character_Model_01/HandRight");
        OvenObject = this.gameObject;
        Burner = this.gameObject.transform.Find("Burner").gameObject;
        ThisCollider = this.gameObject.GetComponent<Collider>();
        UICamera = GameObject.Find("UI/UICamera");
        CameraUIScript m_CameraUIScript = UICamera.GetComponent<CameraUIScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Burner.transform.childCount >= 1)
        {
            OvenInUse = true;
            Utensil = Burner.gameObject.transform.GetChild(0).gameObject;
            UtensilCollider = Utensil.GetComponent<Collider>();
            PickupTest PickupScript = Utensil.gameObject.GetComponent<PickupTest>();
            Rb = Utensil.GetComponent<Rigidbody>();
            UICamera.GetComponent<CameraUIScript>().m_camera.enabled = true;
        }
        if(Burner.transform.childCount == 0)
        {
            OvenInUse = false;
            UICamera.GetComponent<CameraUIScript>().m_camera.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if((collision.gameObject.name == "Character_Model_01") && (OvenInUse == true) && (HandLeft.transform.childCount == 0))
        {
            Debug.Log("GlobalHeld Left ==" + GlobalHeldLeft);

            GlobalHeldLeft = true;
            Utensil.GetComponent<PickupTest>().ThisItemIsBeingCarried = true;

            Rb.useGravity = false;
            Rb.isKinematic = true;
            Utensil.transform.position = HandLeft.transform.position;
            Utensil.transform.rotation = HandLeft.transform.rotation;
            Utensil.transform.SetParent(HandLeft.transform);
            Debug.Log("Pickup Oven");
            StartCoroutine(Delay());

            if((Input.GetKeyDown(KeyCode.Mouse1)) && (GlobalHeldRight == false))
            {
                GlobalHeldLeft = true;
                Utensil.GetComponent<PickupTest>().ThisItemIsBeingCarried = true;

                Rb.useGravity = false;
                Rb.isKinematic = true;
                Utensil.transform.position = HandRight.transform.position;
                Utensil.transform.rotation = HandRight.transform.rotation;
                Utensil.transform.SetParent(HandRight.transform);
                Debug.Log("Pickup Oven");
            }
         }*/
    }
    IEnumerator Delay()
    {
        print(Time.time);
        UtensilCollider.enabled = false;
        yield return new WaitForSeconds(2);
        print(Time.time);
        UtensilCollider.enabled = true;
    }
}