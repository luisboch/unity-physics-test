using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(AutoStabilizer), typeof(PlayerProperties))]
public class PlayerLuis : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    public float power = 10;
    public float rotationVel = 100f;
    public float maxVel = 50;

    private AutoStabilizer stabilizer;
    private PlayerProperties playerProperties;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        stabilizer = GetComponent<AutoStabilizer>();
        playerProperties = GetComponent<PlayerProperties>();
    }

    void Update() {
    }

    void FixedUpdate() {

        if (stabilizer.isStabilized && !playerProperties.isCrashed) {

            var vertical = Input.GetAxis("Vertical");
            var direction = rigidbody2d.velocity;
            var currentVel = direction.magnitude;
            //
            if (vertical > 0 && (currentVel < maxVel || Vector2.Angle(direction.normalized, transform.right) > 20)) {
                rigidbody2d.AddForce(new Vector2(transform.right.x, transform.right.y) * power * vertical);
            }

            var horizontal = Input.GetAxis("Horizontal");
            if ( horizontal != 0) {
                rigidbody2d.angularVelocity = rotationVel * -horizontal;
            }
        }
    }
}
