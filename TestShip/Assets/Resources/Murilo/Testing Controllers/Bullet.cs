using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 10;
    public float velocity = 100;

    private Rigidbody2D rb;
    private Vector3 dir;

    // Use this for initialization
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //Vector3 force = dir * velocity;
        //Debug.Log("Start = "+force);
        //rb.AddForce(force);
        //rb.velocity = dir * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        transform.position += dir * velocity * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir)
    {
        this.dir = dir;
    }
}
