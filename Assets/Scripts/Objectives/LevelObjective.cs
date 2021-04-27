using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LevelObjective : MonoBehaviour {

    public event Action OnObjectiveCollected;

    [Header("References")]
    [SerializeField] private SFXEvent OnCollectSound;

    [Header("States")]
    private bool collected;
    public bool Collected {
        get {
            return collected;
        }
        private set {
            collected = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            Collect();
        }
    }

    private void Collect() {
        if (!Collected) {
            Collected = true;
            if (OnCollectSound != null) { OnCollectSound.Play(); }
            OnObjectiveCollected.Invoke();
            gameObject.SetActive(false);
        }
    }
}
