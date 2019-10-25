using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingLookAtCamera : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 LookAtRot = transform.position - cam.transform.position;
        transform.rotation = Quaternion.LookRotation(LookAtRot, Vector3.up);
    }
}
