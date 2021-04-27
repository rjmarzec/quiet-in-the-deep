using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

    [SerializeField] private float duration;
    private float spawnTime;

    public float Duration {
        get {
            return duration;
        }
        set {
            duration = value;
            spawnTime = Time.time;
            return;
        }
    }

    private void Awake() {
        spawnTime = Time.time;
    }

    void Update() {
        if (Time.time >= spawnTime + duration) {
            Destroy(gameObject);
        }
    }
}
