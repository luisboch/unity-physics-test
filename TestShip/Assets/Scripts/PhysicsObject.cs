﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

	public float gravityModifier = 1f;
	public float minGroundNormalY = 0.65f;

	protected Rigidbody2D rb;

	protected Vector2 targetVelocity;
	protected Vector2 velocity;  
	protected ContactFilter2D contactFilter;
	protected const float minMoveDistance = 0.001f;
	protected const float shellRadius = 0.01f; 

	protected bool grounded;
	protected Vector2 groundNormal;

	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);


	void OnEnable(){
		rb = GetComponent<Rigidbody2D> (); 
	}
	// Use this for initialization
	void Start () {
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
		contactFilter.useLayerMask = true;

	}

	// Update is called once per frame
	void Update () {
		ComputeVelocity ();
	}

	protected virtual void ComputeVelocity(){

	}

	void FixedUpdate(){
		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;
		grounded = false;

		Vector2 movealongGround = new Vector2 (groundNormal.y, -groundNormal.x);
		Vector2 deltaPosition = velocity * Time.deltaTime;


		Vector2 move = movealongGround * deltaPosition.x;
		Movement (move, false);

		move = Vector2.up * deltaPosition.y;
		Movement (move, true);
	}

	void Movement(Vector2 move, bool yMovement){
		float distance = move.magnitude;

		if (distance > minMoveDistance) {
			int count = rb.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
			hitBufferList.Clear ();

			for (int i = 0; i < count; i++) {
				hitBufferList.Add(hitBuffer[i]);
			}

			for (int i = 0; i < hitBufferList.Count; i++) {
				Vector2 currentNormal = hitBufferList [i].normal;
				if (currentNormal.y > minGroundNormalY) {

					grounded = true;

					if (yMovement) {
						groundNormal = currentNormal;
						currentNormal.x = 0;
					} 
				}

				float projection = Vector2.Dot (velocity, currentNormal);

				if (projection < 0) {
					velocity = velocity - projection * currentNormal;
				}

				float modifiedDistance = hitBufferList [i].distance - shellRadius;
				distance = modifiedDistance < distance ? modifiedDistance : distance;
			}
		}

		rb.position = rb.position + move.normalized * distance; 
	}
}