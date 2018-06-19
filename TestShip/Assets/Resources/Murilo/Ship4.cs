using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship4 : MonoBehaviour {

    public float force = 5.0f;
    public float maneuverSpeed = 10.0f;

    private Transform aim;
    private Transform sprite;
    private Rigidbody2D rigid2d;

	// Use this for initialization
	void Start ()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        aim = gameObject.transform.Find("aim");
        sprite = gameObject.transform.Find("ship");
    }
	
	// Update is called once per frame
	void Update ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0)
        {
            Vector3 speed = Vector3.right * force * h * Time.deltaTime;
            transform.Translate(speed);
        }

        if (v != 0)
        {
            Vector3 speed = Vector3.up * force * v * Time.deltaTime;
            transform.Translate(speed);
        }


        float h2 = Input.GetAxis("Horizontal2");
        float v2 = Input.GetAxis("Vertical2");
        if(h2 != 0 || v2 != 0)
        {
            //Debug.Log(h2 +" | "+ v2);
            Vector3 vec3 = new Vector3(h2, v2, 0);
            //Debug.Log(vec3);

            Vector3 myPos = transform.position;
            Vector3 aimPos = myPos + vec3;
            aim.position = aimPos;

            Vector3 vectorToTarget = vec3;

            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            sprite.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * maneuverSpeed);

        }
        //else if(h2 == 0 && v2 == 0)
        //{
        //    aim.position = Vector3.zero;
        //}
    }
    
}
