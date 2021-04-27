using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("References")]
    private PatrolMovement movementPatrol;
    private TrackingMovement movementTracking;

    void Start() {
        movementPatrol = GetComponent<PatrolMovement>();
        movementTracking = GetComponent<TrackingMovement>();

        Patrol();
    }

    public void OnFocusEnter(Collider2D other) {
        if (other.tag == "Player" && Player.instance != null && Player.instance.Stealth != null && !Player.instance.Stealth.Hiding) {
            Player.instance.RegisterTrackingEnemy(this);
            Track(other.transform);
        }
    }

    public void OnFocusStay(Collider2D other) {
        if (other.tag == "Player" && Player.instance != null && Player.instance.Stealth != null && !Player.instance.Stealth.Hiding) {
            Track(other.transform);
        }
    }

    public void OnAttentionRangeExit(Collider2D other) {
        if (other.tag == "Player") {
            Player.instance.RemoveTrackingEnemy(this);
            Patrol();
        }
    }

    private void Patrol() {
        movementPatrol.enabled = true;
        movementTracking.enabled = false;
        movementPatrol.PatrolToClosestPosition();
    }

    private void Track(Transform t) {
        movementPatrol.enabled = false;
        movementTracking.enabled = true;
        movementTracking.SetTarget(t);
    }
}
