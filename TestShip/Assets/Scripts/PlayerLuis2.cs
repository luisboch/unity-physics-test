using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLuis2 : PhysicsObject {

    public float power = 100;
    public float rotateSpeed = 20;
    public float maxVel = 50;

    void Start() {

    }

    protected override void ComputeVelocity() {

        float vertical = Input.GetAxis("Vertical");
        var direction = velocity;
        var velLenght = direction.magnitude;

        Vector2 force = Vector2.zero;
        if (vertical > 0 && (velLenght < maxVel || Vector2.Angle(direction.normalized, transform.up) > 20)) {
            force += new Vector2(transform.up.x, transform.up.y) * vertical;
        }

        velocity += force * Time.deltaTime;

        Debug.Log(transform.up + " --- " + force);
        //        Debug.Log("Force: " + force + "Vel: " + velLenght);

        var horizontal = Input.GetAxis("Horizontal");
        var rotateBy = rotateSpeed;
        rotateBy = rotateBy * -horizontal;

        rotateBy *= Time.deltaTime;

        transform.right = Rotate(transform.right, rotateBy);


        //        if (horizontal == 0f) {
        //            rigidbody2d.angularVelocity = 0;
        //        }
    }
}
