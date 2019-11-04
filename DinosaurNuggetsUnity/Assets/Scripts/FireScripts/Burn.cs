using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    public GameObject parent;
    public Flame flame;

    private void Start() 
    {
        parent = this.gameObject.transform.parent.gameObject;
        flame = parent.GetComponent<Flame>();
    }
   void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag == "Ingredient")
       {
           if(flame.OnFire == true)
           {
                Debug.Log("Burning");
                StartCoroutine(Burning(other));
           }
       }
   }
    public IEnumerator Burning(Collider ingCol)
    {
        Renderer rend = ingCol.gameObject.GetComponent<Renderer>();
        if(rend == null)
        {
            Renderer[] renderers = ingCol.gameObject.GetComponentsInChildren<Renderer>();
            for(int i = 0;i<renderers.Length;i++)
            {
                rend = renderers[i];
                Color oldColor = rend.material.color;
                Debug.Log(oldColor);
                float ElapsedTime = 0.0f;
                float TotalTime = 10.0f;
                while (ElapsedTime < TotalTime) 
                {
                    ElapsedTime += Time.deltaTime;
                    rend.material.SetColor("_BaseColor", Color.black);//color = //Color.black;//Lerp(oldColor, Color.black, (ElapsedTime / TotalTime));
                }
            }
        }
        else
        {
            Color oldColor = rend.material.color;
            Debug.Log(oldColor);
            float ElapsedTime = 0.0f;
            float TotalTime = 10.0f;
            while (ElapsedTime < TotalTime) 
            {
                ElapsedTime += Time.deltaTime;
                rend.material.SetColor("_BaseColor", Color.Lerp(oldColor, Color.gray, (ElapsedTime / TotalTime)));//color = //Color.black;//Lerp(oldColor, Color.black, (ElapsedTime / TotalTime));
            }
        }
        yield return new WaitForSeconds(4);
        //Destroy(ingCol.gameObject);
    }
}
