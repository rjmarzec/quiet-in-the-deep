using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SwimmingMovement : MonoBehaviour {
    public float velocityGainedOnClick = 1.0f;
    public float velocityLostPerSecond = 0.5f;
    public float maxClickingVelocity = 5.0f;

    public float ropeRetreatDuration = 1.0f;
    public float ropeRetreatVelocity = 10.0f;

    private float currentVelocity = 0.0f;
    private int mouseClickNumber = 0;
    private Vector2 mouseDirection;
    private Vector2 currentDirection;
    private Rigidbody2D rb;

    [Header("Sound Effects")]
    [SerializeField] private SFXEvent movementSound;

    private Vector2 ropeDirection = Vector2.zero;
    private float retreatTimer = 0.0f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        AddDrag();
        CheckInput();
        MovePlayer();
    }

    private void AddDrag() {
        // reduce the player's speed by the lostPerSecond value
        currentVelocity = Mathf.Max(0.0f, currentVelocity - (velocityLostPerSecond * Time.deltaTime));
    }

    private void CheckInput() {
        // watch for player input and move the player in that direction on click
        if (Input.GetMouseButtonDown(mouseClickNumber)) {
            // play audio
            if (movementSound != null) { movementSound.Play(); }

            // add some velocity to the player, but not past the cap
            currentVelocity = Mathf.Min(maxClickingVelocity, currentVelocity + velocityGainedOnClick);

            // switch the mouse click input we're checking
            mouseClickNumber = (mouseClickNumber == 0) ? 1 : 0;
        }

        // if the player is scrolling, mark them as retreating
        if (Input.mouseScrollDelta.y != 0) {
            retreatTimer = ropeRetreatDuration;
        }
        retreatTimer = Mathf.Max(0, retreatTimer - Time.deltaTime);
    }

    private void MovePlayer() {
        // compute the normalized direction between the player and their cursor
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = gameObject.transform.position;
        mouseDirection = (mousePosition - playerPosition).normalized;

        currentDirection = (Vector2)(Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.down);

        // compute the velocity vector for the direction the player is clicking to move in
        Vector2 playerClickVelocity = currentVelocity * (currentDirection.normalized + mouseDirection.normalized).normalized;

        // compute the velocity vector for the direction the player is retreating in
        Vector2 playerRetreatVelocity = ropeDirection * ropeRetreatVelocity;

        // give the player velocity as a combination of the retreat velocity and swimming velocity based on
        // how long it was since they last retreated
        float retreatFactor = (retreatTimer / ropeRetreatDuration) * (retreatTimer / ropeRetreatDuration);
        rb.velocity = playerClickVelocity * (1 - retreatFactor) + playerRetreatVelocity * retreatFactor;
    }

    public void UpdateRopeMovement(Vector2 ropeDirectionIn) {
        ropeDirection = ropeDirectionIn;
    }
}
