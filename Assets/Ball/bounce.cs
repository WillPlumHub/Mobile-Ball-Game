using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour {
    public float verticalBounciness = 1f;
    public float horizontalBounciness = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2 normal = collision.contacts[0].normal;

        // Check if the collision is from the top or bottom
        if (Mathf.Abs(normal.y) > Mathf.Abs(normal.x)) {
            // Vertical collision
            Bounce(verticalBounciness, 0f);
        }
        else {
            // Horizontal collision
            Bounce(0f, horizontalBounciness);
        }
    }

    private void Bounce(float verticalBounce, float horizontalBounce) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 newVelocity = new Vector2(rb.velocity.x * horizontalBounce, rb.velocity.y * verticalBounce);

        // Apply the new velocity
        rb.velocity = newVelocity;
    }
}