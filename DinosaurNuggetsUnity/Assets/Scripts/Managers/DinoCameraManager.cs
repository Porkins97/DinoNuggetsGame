using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DinoCameraManager : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;

    public Vector3 minMove = new Vector3();

    private Vector3 initOffset;
    private Vector3 velocity;
    private Camera cam;

    private Bounds bound;

    void Start()
    {
        initOffset = transform.position;
        cam = GetComponent<Camera>();
        
    }


    private void LateUpdate()
    {
        if(targets.Count == 0)
            return;
        
        Move();
        Zoom();
    }

    private void Move () 
    {
        bound = new Bounds(initOffset, minMove);

        Vector3 centrePoint = GetCentrePoint2();
        Vector3 newPos = centrePoint + offset + initOffset;

        newPos = new Vector3(Mathf.Clamp(newPos.x, bound.min.x, bound.max.x),
                                                        Mathf.Clamp(newPos.y, bound.min.y, bound.max.y),
                                                        Mathf.Clamp(newPos.z, bound.min.z, bound.max.z));

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);

        /*
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bound.min.x, bound.max.x),
                                                        Mathf.Clamp(transform.position.y, bound.min.y, bound.max.y),
                                                        Mathf.Clamp(transform.position.z, bound.min.z, bound.max.z));
                                                        */
    }

    private void Zoom () 
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, Remap(GetGreatestDistance(), 0f, 10f, 0f, 1f));
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    //https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
     public float Remap (float from, float fromMin, float fromMax, float toMin,  float toMax)
    {
        var fromAbs  =  from - fromMin;
        var fromMaxAbs = fromMax - fromMin;      
       
        var normal = fromAbs / fromMaxAbs;
 
        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;
 
        var to = toAbs + toMin;
       
        return to;
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }

    Vector3 GetCentrePoint2()
    {
        Vector3 placement = targets[0].position - targets[1].position;
        placement = placement * -0.5f;
        return placement;
    }

    Vector3 GetCentrePoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
