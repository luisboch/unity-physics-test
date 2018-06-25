using UnityEngine;

[RequireComponent(typeof(PlayerProperties))]
public class CrashController : MonoBehaviour {

    private PlayerProperties properties;
    public float powerModifier = 1f;

    void Start() {
        properties = GetComponent<PlayerProperties>();
    }

    void Update() {

    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
        float power = 0f;

        for (int i = 0; i < collisionInfo.contactCount; i++) {
            var contact = collisionInfo.GetContact(i);
            power += contact.normalImpulse;
        }

        properties.hit(power * powerModifier);
    }
}
