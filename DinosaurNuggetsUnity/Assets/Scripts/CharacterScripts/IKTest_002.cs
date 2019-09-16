using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTest_002 : MonoBehaviour
{
    public int ChainLength = 1;
    public Transform Target;
    public Transform Pole;

    public int Iterations = 10;
    public float Delta = 0.001f;
    [Range(0,1)] public float SnapBackStrength = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        var current = this.transform;
        for(int i = 0; i < ChainLength && current != null && current.parent != null; i++)
        {
            current = current.parent;
        }
    }
}
