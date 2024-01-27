using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class goomba : MonoBehaviour {

    public float moveDistance = 5f;        // Distance the enemy moves left and right
    public float moveSpeed = 2f;           // Speed of the enemy's movement
    public float shootingInterval = 2f;    // Time interval between shots
    public GameObject projectilePrefab;    // Projectile to be shot
    public Transform shootPoint;           // Point from where the projectile is shot

    private float originalX;               // Initial X position of the enemy
    private float timeSinceLastShot;       // Time passed since the last shot
    public float launchForce = 10f;

    public movement mov;

    void Start() {
        originalX = transform.position.x;
    }

    void Update() {
        MoveLeftAndRight();
        ShootProjectiles();
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.CompareTag("Player") && mov.isMaterialActive == true) {
            Destroy(gameObject);
        }
    }

    void MoveLeftAndRight() {
        // Calculate the target X position based on the movement distance
        float targetX = originalX + Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = new Vector2(targetX, transform.position.y);
    }

    void ShootProjectiles() {
        // Update the time since the last shot
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= shootingInterval) {
            timeSinceLastShot = 0f;
            Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        }
    }
}