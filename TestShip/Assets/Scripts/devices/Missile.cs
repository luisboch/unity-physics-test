using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : DeviceBase {

    [Header("Velocidade do míssil ao dispara-lo")]
    public float speed;

    override protected void Start() {
        base.Start();
    }

    override protected void Update() {
        base.Update();
        if (this.enabled) {
            this.rd.velocity = transform.up * speed;
        }
    }
}