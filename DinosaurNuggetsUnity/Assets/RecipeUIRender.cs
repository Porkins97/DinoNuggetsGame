using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUIRender : MonoBehaviour
{
    Image m_Image;
    private Sprite default_Sprite;
    public Sprite m_Sprite1;
    public Sprite m_Sprite2;
    public Sprite m_Sprite3;
    public float progress;
 

    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<Image>();
        Recipe RecipeScript = GameObject.Find("Character_Model_01").GetComponent<Recipe>();
        default_Sprite = m_Image.sprite;
        progress = GameObject.Find("Character_Model_01").GetComponent<Recipe>().progress;
    }

    // Update is called once per frame
    void Update()
    {
        progress = GameObject.Find("Character_Model_01").GetComponent<Recipe>().progress;
        //Debug.Log("progress == " + progress);
        if(this.gameObject.name == "LeftImage")
        {
            //Debug.Log("This Is Image 1");
            if (progress > 0f)
            {
                m_Image.sprite = m_Sprite1;
            }
            else
                m_Image.sprite = default_Sprite;
        }
        if (this.gameObject.name == "CentreImage")
        {
            //Debug.Log("This is image 2");
            if (progress > 0.5f)
            {
                m_Image.sprite = m_Sprite2;
                //Debug.Log("Change to image 2");
            }
            else
                m_Image.sprite = default_Sprite;
        }
        if (this.gameObject.name == "RightImage")
        {
            if (progress == 1f)
            {
                m_Image.sprite = m_Sprite3;
                //Debug.Log("Change to image 3");
            }
            else
                m_Image.sprite = default_Sprite;
        }
    }
}
