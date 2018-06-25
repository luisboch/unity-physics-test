using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerProperties))]
public class AutoStabilizer : MonoBehaviour {

    public float minVelocityToActivate = 50;
    public float minRotationToActivate = 200;
    public float stableRotation = 7f;

    public float stabilizerForce = 5f;

    public bool isStabilized { get; private set; }

    public GameObject activeIcon;

    private bool working = false;
    private bool activationControl = false;

    private PlayerProperties properties;

    private Rigidbody2D rigidbody2d;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        properties = GetComponent<PlayerProperties>();
    }

    void Update() {
        if (activeIcon) {
            activeIcon.SetActive(!isStabilized);
        }
    }

    void OnCollisionEnter2D() {
        working = true;
        activationControl = true;
        isStabilized = false;
    }

    void FixedUpdate() {
        if (working && enabled && !properties.isCrashed) {
            var abs = Mathf.Abs(rigidbody2d.angularVelocity);
            var vel = rigidbody2d.velocity.magnitude;

            if (activationControl) {
                activationControl = false;
                if (vel < minVelocityToActivate && abs < minRotationToActivate) {
                    working = false; isStabilized = true;
                }
            }

            if (working) {
                if (abs < stableRotation) {
                    rigidbody2d.angularVelocity = 0;
                    working = false;
                    isStabilized = true;
                } else {
                    rigidbody2d.angularVelocity -= (rigidbody2d.angularVelocity * stabilizerForce * Time.fixedDeltaTime);
                }
            }
        } else {
            isStabilized = true;
        }
    }

}
