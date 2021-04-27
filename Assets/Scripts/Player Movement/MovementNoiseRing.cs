using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class MovementNoiseRing : MonoBehaviour {

    public Rigidbody2D movingObjectRB;
    private Light2D detectionLight;

    [SerializeField] private float multiplier = 1f;

    void Start() {
        detectionLight = GetComponent<Light2D>();
    }

    void Update() {
        float factor = (movingObjectRB.velocity.magnitude * multiplier) + .75f;
        transform.localScale = Vector3.one * factor;
        detectionLight.pointLightOuterRadius = factor;
    }
}
