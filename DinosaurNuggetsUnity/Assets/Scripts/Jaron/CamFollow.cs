using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    [SerializeField] private Transform follow;
    private Transform myCamera;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Transform>();
        follow = follow.GetComponent<Transform>();

        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float smooth = 3f;
        Vector3 trans = follow.position;
        Vector3 temp = new Vector3(trans.x + offset.x, offset.y, trans.z + offset.z);
        transform.position = temp;

    }
}
