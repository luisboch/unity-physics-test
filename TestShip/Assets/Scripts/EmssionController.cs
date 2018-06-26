using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class EmssionController : MonoBehaviour {
    private ParticleSystem system;
    private PlayerProperties properties;
    private float initialValue;

    void Start() {
        system = GetComponent<ParticleSystem>();
        GameObject go = gameObject;



        while (!properties && go) {
            properties = go.GetComponent<PlayerProperties>();
            go = transform.parent ? transform.parent.gameObject : null;
        }

        if (system) {
            initialValue = system.emission.rateOverTime.constant;
        }
    }

    void Update() {
        var emission = system.emission;
        emission.rateOverTime = initialValue - (initialValue / properties.initialHealth * properties.currentHealth);
    }
}
