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

        for (int i = 0; i < collisionInfo.contacts.Length; i++) {
            var contact = collisionInfo.contacts[i];
            power += contact.normalImpulse;
        }

        properties.hit(power * powerModifier);
    }
}
