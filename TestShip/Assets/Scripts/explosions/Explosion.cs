using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Explosion : MonoBehaviour {

    [Header("Tamanho que a explosao vai ficar")]
    [Tooltip("0 não terá tamanho, 1=1x do sprite,1.6=1.6x do sprite, 2=2x do sprite")]
    public float scaleTo = 1f;

    [Header("Tempo da explosao (e o tempo que levara para chegar à escala definida, partindo de zero")]
    public float lifeTime = 1f;

    [Header("Curva da escala, durante a explosao (máx 1f)")]
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    [Header("Quanto esta explosao vai aplicar de dano ao alvo")]
    public float damage = 5f;

    /// <summary>
    /// Representa o tempo de vida desta instancia
    /// </summary>
    private float curLife = 0f;

    [Header("GO que gerou esta explosion ( setado a partir do projétil)")]
    public GameObject owner;

    void Start() {
        transform.localScale = Vector3.zero;
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    void Update() {
        curLife += Time.deltaTime;
        float curScale = curve.Evaluate(curLife) * scaleTo;
        transform.localScale = new Vector3(curScale, curScale, curScale);

        if (curLife > lifeTime) {
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other) {
        float power = curve.Evaluate(curLife);
        if (other != null && other.gameObject != null) {
            if (owner != other.gameObject) {
                PlayerProperties pl = other.gameObject.GetComponent<PlayerProperties>();

                if (pl) {
                    pl.hit(power * damage, owner);
                }
            }
        }
    }
}