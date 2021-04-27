using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolMovement : MonoBehaviour {

    [System.Serializable]
    public class PatrolPosition {
        public Transform transform;
        public float stallDuration;
    }

    [Header("Settings")]
    [SerializeField] private float speed;

    [SerializeField] private List<PatrolPosition> patrolPositions;

    [Header("References")]
    private Rigidbody2D rigidbody;
    private Rigidbody2D Rigidbody {
        get {
            if (rigidbody == null) { rigidbody = GetComponent<Rigidbody2D>(); }
            if (rigidbody == null) { rigidbody = gameObject.AddComponent<Rigidbody2D>(); }
            return rigidbody;
        }
    }

    [Header("Cache")]
    private bool stalling;
    private int currentIndex;
    private Vector3 directionAtPositionAdvance;
    private float timeOfPositionAdvance;

    void Start() {
        currentIndex = 0;
    }

    private void OnEnable() {
        stalling = false;
        timeOfPositionAdvance = Time.time;
        directionAtPositionAdvance = Vector3.zero;
    }

    private void OnDisable() {
        stalling = false;
    }

    void FixedUpdate() {
        if (patrolPositions.Count == 0) { return; }

        if (stalling) { return; }

        if (Vector3.Distance(transform.position, patrolPositions[currentIndex].transform.position) < .5f) {
            StopAllCoroutines();
            StartCoroutine(AdvancePosition());
            Rigidbody.velocity = Vector3.zero;
        }

        Vector3 vectorToTarget = patrolPositions[currentIndex].transform.position - transform.position;

        float percent = Mathf.Clamp01(Time.time - timeOfPositionAdvance);

        Vector3 movementVector = ((vectorToTarget * percent) + (directionAtPositionAdvance * (1 - percent))).normalized;

        Rigidbody.velocity = movementVector.normalized * speed * Time.deltaTime;
    }

    private IEnumerator AdvancePosition() {
        stalling = true;
        yield return new WaitForSeconds(patrolPositions[currentIndex].stallDuration);
        stalling = false;
        currentIndex++;
        timeOfPositionAdvance = Time.time;
        directionAtPositionAdvance = Rigidbody.velocity.normalized;
        if (currentIndex >= patrolPositions.Count) {
            currentIndex = 0;
        }
    }

    public void PatrolToClosestPosition() {
        if (patrolPositions.Count == 0) { return; }
        int minIndex = -1;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < patrolPositions.Count; i++) {
            float distance = Vector3.Distance(transform.position, patrolPositions[i].transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                minIndex = i;
            }
        }
        currentIndex = minIndex;
        timeOfPositionAdvance = Time.time;
        directionAtPositionAdvance = Rigidbody.velocity.normalized;
    }
}
