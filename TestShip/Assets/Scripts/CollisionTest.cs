using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
public class CollisionTest : MonoBehaviour {

    List<DrawPoint> draws = new List<DrawPoint>();


    public bool stopOnDraw = false;
    public float drawSphereSize = 0.2f;
    public float drawLineSize = 0.4f;

    void OnDrawGizmos() {
        for (int i = 0; i < draws.Count; i++) {
            DrawPoint p = draws[i];
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(p.point.point, drawSphereSize);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(p.point.point, p.point.point + (p.point.normal.normalized * drawLineSize));
        }
    }


    void Update() {
        for (int i = 0; i < draws.Count; i++) {
            DrawPoint p = draws[i];
            if (p.deadTime < Time.time) {
                draws.Remove(p);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        Debug.Log(collisionInfo.relativeVelocity);

        //for (int i = 0; i < collisionInfo.contactCount; i++) {
        //    var contact = collisionInfo.GetContact(i);
        //    draws.Add(new DrawPoint(contact));
        //}

        if (stopOnDraw) {
            Time.timeScale = 0;
        }

    }

    private class DrawPoint {
        public ContactPoint2D point;
        public float deadTime = Time.time + 3f;

        public DrawPoint(ContactPoint2D point) {
            this.point = point;
        }
    }
}
