using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DinoMove : MonoBehaviour
{
    public float movementSpeed;

        Rigidbody rb;
        public float walkspeed = 1.0F;
        public float turnspeed = 1.0f;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	void Update()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		transform.position += Vector3.right * h * walkspeed * Time.deltaTime;
		transform.position += Vector3.forward * v * walkspeed * Time.deltaTime;
		//transform.Translate(0f, 0f, v * walkspeed * Time.deltaTime);
		//transform.Translate(h * walkspeed * Time.deltaTime, 0f, 0f );

		//transform.Rotate(0f, turnspeed*h * Time.deltaTime, 0f, Space.Self);
		
		if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
		{
			float angle = Mathf.Atan2(h, v);
			//Debug.Log(angle);
			transform.localRotation = Quaternion.Euler(0.0f, Mathf.Rad2Deg*angle, 0.0f);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("DinoNuggetsPrototypeScene2");
		}
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		/*
			if (Input.GetKey ("w"))
			{
				transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed;
			}
			else if (Input.GetKey ("s"))
			{
				transform.position -= transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed;
			}
			
			if (Input.GetKey ("a") && !Input.GetKey ("d"))
			{
				transform.position += transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed;
			}
			else if (Input.GetKey ("d") && !Input.GetKey ("a"))
			{
				transform.position -= transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed;
			}
		*/
	}
				
}
