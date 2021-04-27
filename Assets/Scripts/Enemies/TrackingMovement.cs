using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrackingMovement : MonoBehaviour {

    private Transform target;

    [Header("Settings")]
    [SerializeField] private float speed;

    [Header("References")]
    private Rigidbody2D rigidbody;
    private Rigidbody2D Rigidbody {
        get {
            if (rigidbody == null) { rigidbody = GetComponent<Rigidbody2D>(); }
            if (rigidbody == null) { rigidbody = gameObject.AddComponent<Rigidbody2D>(); }
            return rigidbody;
        }
    }

    void FixedUpdate() {
        if (target == null) { return; }

        Vector3 vectorToTarget = target.position - transform.position;

        Rigidbody.velocity = vectorToTarget.normalized * speed * Time.deltaTime;
    }

    public void SetTarget(Transform t) {
        target = t;
    }
}
