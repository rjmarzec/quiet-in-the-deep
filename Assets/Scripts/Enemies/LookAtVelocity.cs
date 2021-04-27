using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LookAtVelocity : MonoBehaviour {

    [Header("References")]
    private Rigidbody2D rigidbody;
    private Rigidbody2D Rigidbody {
        get {
            if (rigidbody == null) { rigidbody = GetComponent<Rigidbody2D>(); }
            if (rigidbody == null) { rigidbody = gameObject.AddComponent<Rigidbody2D>(); }
            return rigidbody;
        }
    }

    void Update() {
        transform.right = Rigidbody.velocity;
    }
}
