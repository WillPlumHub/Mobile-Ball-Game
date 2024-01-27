using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class move : MonoBehaviour {

    #region Variables
    [Header("Movement Variables")]
    public bool movement;
    public float moveSpeed;
    [Range(-1, 1)]
    public int dir;
    Vector2 moveVector;
    private Rigidbody2D rb;
    [Range(1, 10)]
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool wallJumping;

    public LayerMask groundLayer;

    [Header("Collision Bools")]
    public bool grounded;
    public bool walled;
    public bool rightWalled;
    public bool leftWalled;
    public int wallDir;

    [Header("Circle Collider Variables")]
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;



    [Header("Raycast Variables")]
    public float topCastOffset;
    public float bottomCastOffset;
    public float rayDist;
    #endregion

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        movement = true;
        moveVector = new Vector2(1f, 0f);
    }

    void Update() {
        moveMent();
        vaultCheck();
        collisionChecks();
    }

    void moveMent() {

        if (movement && !walled && !wallJumping) {      //Running Forwards
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime * dir);
            //rb.velocity = moveVector * moveSpeed * Time.deltaTime;
        }

        if (grounded) {     //Bool Management
            wallJumping = false;
        }

        if (Input.GetMouseButtonDown(1) && grounded) {      //Jumping
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
        if (rb.velocity.y < 0) {        //Better Jumping Physics
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetMouseButton(1)) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (!grounded && walled && Input.GetMouseButtonDown(1)) {       //Wall Jumping
            wallJumping = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(1f * (jumpVelocity * wallDir), 1f);
        }

    }

    void vaultCheck() {
        if (dir == -1) {        //Top ray-cast
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(0f, topCastOffset), transform.TransformDirection(Vector2.right), rayDist, groundLayer);
            Debug.DrawRay(transform.position + new Vector3(0f, topCastOffset, 0f), transform.TransformDirection(Vector2.right) * rayDist, Color.red);
        }
        else {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(0f, topCastOffset), transform.TransformDirection(Vector2.left), rayDist, groundLayer);
            Debug.DrawRay(transform.position + new Vector3(0f, topCastOffset, 0f), transform.TransformDirection(Vector2.left) * rayDist, Color.red);
        }

        if (dir == -1) {        //Bottom ray-cast
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(0f, bottomCastOffset), transform.TransformDirection(Vector2.right), rayDist, groundLayer);
            Debug.DrawRay(transform.position + new Vector3(0f, bottomCastOffset, 0f), transform.TransformDirection(Vector2.right) * rayDist, Color.red);
        }
        else {
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(0f, bottomCastOffset), transform.TransformDirection(Vector2.left), rayDist, groundLayer);
            Debug.DrawRay(transform.position + new Vector3(0f, bottomCastOffset, 0f), transform.TransformDirection(Vector2.left) * rayDist, Color.red);
        }
    }

    void collisionChecks() {
        grounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        //walled = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        rightWalled = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        leftWalled = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        walled = rightWalled || leftWalled;
        wallDir = rightWalled ? -1 : 1;
    }

    void OnDrawGizmos() {
         Gizmos.color = Color.red;

         //var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

         Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
         Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
         Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
     }
}
