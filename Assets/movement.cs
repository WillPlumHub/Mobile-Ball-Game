using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class movement : MonoBehaviour
{
    public float slowRate;
    public Rigidbody2D rb;

    public Transform respawn;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            player.transform.position = respawn.position;
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
        if (Input.GetMouseButton(0) && rb.drag < 10) {
            
            rb.drag += Time.deltaTime * slowRate;
            rb.gravityScale = 0;

        } else if (Input.GetMouseButtonUp(0)) {
            rb.drag = 0;
            rb.gravityScale = 1;
        }
    }
}
