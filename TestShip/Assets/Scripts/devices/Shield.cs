using UnityEngine;

public class Shield : DeviceBase {

    public float resistance;

    void Start() {
        base.Start();
    }


    void Update() {
        // As extension of DeviceBase, we need to check if this device is alive (lifetime).
        base.Update();
        if ( owner) {
            PlayerProperties pl = owner.GetComponent<PlayerProperties>();
            pl.shieldActivated = true;
        }
    }

    public void hit(float power, GameObject from) {
        resistance -= power;
        if (resistance <= 0) {
            DestroyMySelf();
        }
    }

    protected override void DestroyMySelf() {
        base.DestroyMySelf();
        PlayerProperties pl = owner.GetComponent<PlayerProperties>();
        pl.shieldActivated = false;
        resistance = 0;
    }
}
