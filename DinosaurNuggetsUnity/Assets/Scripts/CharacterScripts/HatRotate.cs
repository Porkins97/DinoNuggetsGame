using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatRotate : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    private float val;
    private Renderer ren = null;

    void Start()
    {
        ren = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        val += Time.deltaTime * speed;
        if(val >= 360f)
            val = 0f;
        ren.sharedMaterial.SetFloat("_rotationVal", val);
 
    }
}
