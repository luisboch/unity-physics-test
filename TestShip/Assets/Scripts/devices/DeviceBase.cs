using UnityEngine;

public class DeviceBase : MonoBehaviour {

    public int ammunitionQuantity;

    [Header("Tempo para a recarga [segundos]")]
    public float reloadTime;

    [Header("Distancia inicial do jogador, 0 é centro.")]
    [Tooltip("Permite configurar a distancia inicial do jogador na direcao que ele está observando, será utilizado, apenas, na criação do GO")]
    public float distanceFromPlayer = 0f;


    [Header("Tempo de vida máximo do objeto (0 não se autodestroy) [segundos] ")]
    [Tooltip("Permite configurar a o tempo de vida do objeto e, apos este tempo passar o metodo \"DestroyMySelf\" será invocado")]
    public float lifeTime = 5f;

    private bool autoDestroy = true;

    protected Rigidbody2D rd;

    public GameObject owner;

    protected virtual void Start() {
        rd = GetComponent<Rigidbody2D>();
        autoDestroy = lifeTime > 0;
    }

    protected virtual void Update() {
        if (autoDestroy) {
            this.lifeTime -= Time.deltaTime;
            if (this.lifeTime < 0) {
                DestroyMySelf();
            }
        }
    }

    protected virtual void DestroyMySelf() {
        Destroy(this.gameObject);
    }
}
