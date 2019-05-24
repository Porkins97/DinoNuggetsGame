using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUIScript : MonoBehaviour
{
    public Camera m_camera;
    public GameObject m_oven;
    public Transform pot;
    public GameObject burner;
    public GameObject UIElements;

    // Start is called before the first frame update
    void Start()
    {
        m_camera.enabled = false;
        pot = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(burner.transform.childCount > 0)
        {
            pot = burner.transform.GetChild(0);
        }

        if (pot != null)
        {
            Vector3 screenPos = m_camera.WorldToScreenPoint(pot.position);
            //Debug.Log("Pot is " + screenPos.x + "Pixels from the left");
            //UIElements.transform.position = screenPos;
        }
    }    
    

}
