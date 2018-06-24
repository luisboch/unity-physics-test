using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship1 : MonoBehaviour {

    public float force = 5.0f;
    public float maneuverSpeed = 10.0f;
    private Rigidbody2D rigid2d;

	// Use this for initialization
	void Start ()
    {
        rigid2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigid2d.AddRelativeForce(new Vector2(force, 0.0f));
        }

        float h = Input.GetAxis("Horizontal");
        if(h != 0)
        {
            Vector3 dir = h < 0 ? Vector3.forward : Vector3.back;
            transform.Rotate(dir * maneuverSpeed * Time.deltaTime);
        }
	}
    
}
