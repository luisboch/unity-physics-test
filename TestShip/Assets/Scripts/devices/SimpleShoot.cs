using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleShoot : DeviceBase {

    [Header("Velocidade do míssil ao dispara-lo")]
    public float speed;

    public Explosion explosion;

    override protected void Start() {
        base.Start();
    }

    override protected void Update() {
        base.Update();
        if (this.enabled) {
            this.rd.velocity = transform.up * speed;
        }
    }


    void OnCollisionEnter2D(Collision2D collisionInfo) {
        // Ao colidir, vamos gerar a explosao
        this.DestroyMySelf();
    }

    /// <summary>
    /// Gera a explosao definida, este metodo pode ser invocado ao colidir com outro objeto, ou ao atingir o seu tempo de vida
    /// </summary>
    protected override void DestroyMySelf() {
        if (explosion) {
            var created = Instantiate(explosion.gameObject);
            created.transform.position = transform.position;
            Explosion expl = created.GetComponent<Explosion>();
            expl.owner = this.owner;
            created.transform.parent = transform.parent;
        }

        base.DestroyMySelf();
    }
}