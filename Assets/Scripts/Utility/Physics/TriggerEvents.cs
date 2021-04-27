using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour {

    [SerializeField] private UnityEvent<Collider> onTriggerEnter;
    [SerializeField] private UnityEvent<Collider> onTriggerStay;
    [SerializeField] private UnityEvent<Collider> onTriggerExit;

    private void OnTriggerEnter(Collider other) {
        onTriggerEnter?.Invoke(other);
    }

    private void OnTriggerStay(Collider other) {
        onTriggerStay?.Invoke(other);
    }

    private void OnTriggerExit(Collider other) {
        onTriggerExit?.Invoke(other);
    }

}
