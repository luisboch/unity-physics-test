using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : DeviceBase {

    [Header("Velocidade do míssil ao dispara-lo")]
    public float speed;

    [Header("Objeto que será gerado quando este projétil atingir um alvo")]
    public Explosion explosion;

    [Header("Posição onde será gerada a explosão (ignorada quando vazia)")]
    public Transform explosionPoint;

    override protected void Start() {
        base.Start();
    }

    override protected void Update() {
        base.Update();
        if (this.enabled) {
            this.rd.velocity = transform.right * speed;
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

            if (explosionPoint) {
                created.transform.position = explosionPoint.position;
            } else {
                created.transform.position = transform.position;
            }
            Explosion expl = created.GetComponent<Explosion>();
            expl.owner = this.owner;
            created.transform.parent = transform.parent;
        }

        base.DestroyMySelf();
    }
}