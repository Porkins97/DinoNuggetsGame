using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraUIScript : MonoBehaviour
{
    public Camera m_camera;
    public GameObject m_oven;
    public Transform pot;
    public GameObject burner;
    public GameObject Recipe;
    public GameObject Title;
    private bool deletetitle = false;
    public Text WinText;

    // Start is called before the first frame update
    void Start()
    {
        pot = null;
        m_camera.enabled = true;
        WinText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((deletetitle == false) &&(Input.anyKeyDown))
        {
            GameObject.Destroy(Title);
            deletetitle = true;
        }

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
    
    public void WinUI()
    {
        WinText.enabled = true;
        Debug.Log("Test Test Test");
    }
}
