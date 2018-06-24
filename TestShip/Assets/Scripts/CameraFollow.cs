using UnityEngine;

public class CameraFollow : MonoBehaviour{

    public Transform target;

    public float camSpeed = 5.0f;

    public bool followRotation = false;


    void Start() {
    }

    void Awake() {
    }


    void LateUpdate() {

        //transform.position = target.position;
        var lerpPos = (target.position - transform.position) * Time.deltaTime * camSpeed;
        transform.position += new Vector3(lerpPos.x, lerpPos.y, 0);

        if (followRotation) {
            transform.rotation = target.rotation;
        }
    }
}
