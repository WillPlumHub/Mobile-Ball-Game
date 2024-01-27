using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UIElements;

public class launcher : MonoBehaviour {

    public float launchForce = 10f;

    private void OnTriggerEnter2D(Collider2D other) {
        // Check if the entering collider is the player
        if (other.CompareTag("Player")) {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            if (playerRb != null) {
                // Apply force to launch the player
                //playerRb.AddForce(transform.up * launchForce, ForceMode2D.Impulse);
                playerRb.velocity = new Vector2(0.0f, launchForce);
            }
        }
    }
}