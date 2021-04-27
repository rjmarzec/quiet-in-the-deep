using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents2D : MonoBehaviour {

    [SerializeField] private UnityEvent<Collider2D> onTriggerEnter;
    [SerializeField] private UnityEvent<Collider2D> onTriggerStay;
    [SerializeField] private UnityEvent<Collider2D> onTriggerExit;

    private void OnTriggerEnter2D(Collider2D other) {
        onTriggerEnter?.Invoke(other);
    }

    private void OnTriggerStay2D(Collider2D other) {
        onTriggerStay?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        onTriggerExit?.Invoke(other);
    }
}
