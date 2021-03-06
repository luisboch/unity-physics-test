using UnityEngine;

public class PlayerProperties : MonoBehaviour {

    public float initialHealth = 100;
    public float currentHealth;
    public float onlineRestoreFactor = 3f;
    public float restoreFactor = 10f;
    public float waitAfterHitRestore = 10f;

    public bool canRestore { get; private set; }

    public bool isCrashed { get; private set; }

    private float lastHit = 0f;

    public bool shieldActivated = false;

    void Start() {
        currentHealth = initialHealth;
    }

    void Update() {
        restore();

        if (lastHit < (Time.time - waitAfterHitRestore)  ) {
            canRestore = true;
        }
    }

    public void restore() {
        if (canRestore && currentHealth < initialHealth) {
            float factor = isCrashed ? restoreFactor : onlineRestoreFactor;
            if (factor > 0f) {
                currentHealth = Mathf.Min(initialHealth, (currentHealth + (factor * Time.deltaTime)));
            }

            if (isCrashed) {
                isCrashed = initialHealth != currentHealth;
            }

        }
    }

    public void hit(float power, GameObject owner) {
        this.currentHealth -= power;
        if (this.currentHealth < 0) {
            isCrashed = true;
            canRestore = true;
            currentHealth = 0;
        } else {
            lastHit = Time.time;
            canRestore = false;
        }
    }

}
