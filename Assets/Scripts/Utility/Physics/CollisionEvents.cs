using UnityEngine;
using UnityEngine.Events;

public class CollisionEvents : MonoBehaviour {

    [SerializeField] private UnityEvent<Collision> onCollisionEnter;
    [SerializeField] private UnityEvent<Collision> onCollisionStay;
    [SerializeField] private UnityEvent<Collision> onCollisionExit;

    private void OnCollisionEnter(Collision collision) {
        onCollisionEnter?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision) {
        onCollisionStay?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision) {
        onCollisionExit?.Invoke(collision);
    }

}
