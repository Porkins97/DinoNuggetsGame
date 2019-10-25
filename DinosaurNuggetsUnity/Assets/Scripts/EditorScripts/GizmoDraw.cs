using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDraw : MonoBehaviour
{
    [SerializeField] public Color colour = Color.blue;
    [SerializeField] public float radius = 0.2f;
    [SerializeField] public bool Box = false;
    [SerializeField] public Vector3 boxPos = new Vector3(1, 1, 1);
    [SerializeField] public Vector3 boxSize = new Vector3(1, 1, 1);
    //[SerializeField] private float boxRadius = 0.1f;
    private void OnDrawGizmos()
    {
        Gizmos.color = colour;
        //Gizmos.matrix = Matrix4x4.Rotate(transform.rotation);
        Gizmos.matrix = transform.localToWorldMatrix;
        if (Box)
        {
            Gizmos.DrawWireCube(boxPos, boxSize);
        }
        else
        {
            Gizmos.DrawWireSphere(Vector3.zero, radius);
        }
        
        /*
       if(transform.childCount > 0) {
            Transform child = transform.GetChild(0);
            Gizmos.matrix = Matrix4x4.TRS(transform.position, 
                Quaternion.FromToRotation(Vector3.up, transform.position + child.position), 
                new Vector3(0.1f, Vector3.Distance(transform.position, child.position), 0.1f));
            Gizmos.DrawWireCube((child.position - transform.position) / 2f, new Vector3(boxRadius, boxRadius, ((child.position + transform.position)).z));
        }*/
    }
}
