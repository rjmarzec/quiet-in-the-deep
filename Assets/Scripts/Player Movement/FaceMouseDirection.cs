using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouseDirection : MonoBehaviour
{
    public float angleOffset = 0f;

    void Update()
    {
        // grab the world position of the mouse
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // grab the position of the player
        Vector2 playerPosition = gameObject.transform.position;

        // compute the normalized direction of direction from the mouse to the player
        Vector2 mouseDirection = (mousePosition - playerPosition).normalized;

        // compute the angle of the vector with respect to the vector (1, 0)
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg + angleOffset;

        // rotate the object in the direction of the mouse
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
