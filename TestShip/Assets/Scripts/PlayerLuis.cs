using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLuis : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    public float power = 100;
    public float rotateSpeed = 20;
    public float maxVel = 50;
    private Vector2 direction;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update() {

    }

    void FixedUpdate() {
        var vertical = Input.GetAxis("Vertical");
        var direction = rigidbody2d.velocity;
        var currentVel = direction.magnitude;

        if (vertical > 0 && (currentVel < maxVel || Vector2.Angle(direction.normalized, transform.up) > 20)) {
            rigidbody2d.AddForce(new Vector2(transform.up.x, transform.up.y) * power * vertical);
        }

        Debug.Log("UP: " + transform.up + "Vel: " + currentVel);

        var horizontal = Input.GetAxis("Horizontal");
        var rotateBy = rotateSpeed;
        rotateBy = rotateBy * -horizontal;
        rigidbody2d.MoveRotation(rigidbody2d.rotation + rotateBy * Time.fixedDeltaTime);

        if (horizontal == 0f) {
            rigidbody2d.angularVelocity = 0;
        }
    }
}
