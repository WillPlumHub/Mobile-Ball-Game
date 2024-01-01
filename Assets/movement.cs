using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class movement : MonoBehaviour {

    public energy E;

    public float slowRate;
    public float boost;
    public float boostRate;
    public Rigidbody2D rb;

    public GameObject player;
    public Transform spawn;

    public Vector2 startTouchPosition;
    public Vector2 endTouchPosition;
    public Vector2 direction;
    public float speed;
    public float min;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(4.0f, 2.0f);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) { //Respwn on RMB
            player.transform.position = spawn.position;
            rb.velocity = new Vector2(0.0f, 0.0f);
        }

        if (Input.GetMouseButton(0) && rb.drag < 10) { //Slow on touch
            rb.drag += Time.deltaTime * slowRate;
            rb.gravityScale = 0;
            boost += Time.deltaTime * boostRate;
        } else if (Input.GetMouseButtonUp(0)) {
            rb.drag = 0;
            rb.gravityScale = 1;
            rb.velocity = new Vector2(0.0f, boost);
            boost = 0;
        }

        //Swipe controls
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began) {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended) {
            endTouchPosition = Input.GetTouch(0).position;
            if (E.en > 0 && Vector2.Distance(endTouchPosition, startTouchPosition) > min) {
                direction = (endTouchPosition - startTouchPosition).normalized;
                rb.velocity = direction * speed;
                E.en--;
            }
        }
    }
}
