using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.IK;
using DG.Tweening;
using System.Diagnostics;
using UnityEngine.Splines;


public class movement : MonoBehaviour {

    
    public float x;
    public float y;
    public float z;
    public float A;
    
    
    public energy E;

    public float slowRate;
    public float boost;
    public float boostRate;
    public Rigidbody2D rb;

    public GameObject player;
    public Transform spawn;

    public Vector2 startTouchPosition;
    public Vector2 endTouchPosition;
    private Vector2 direction;
    public float speed;
    public float min;
    public bool canDash;

    public Material newMaterial;
    public float changeDuration = 3.0f;
    private Renderer objectRenderer;
    public bool isMaterialActive;
    private float timer;

    public float wallBoost;
    public float wallLim; 
    public float xLim;
    private float temp;

    public hazzard h;

    public SplineContainer spline;

    //public int x;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        //player = GetComponent<GameObject>();
        rb.velocity = new Vector2(4.0f, 2.0f);
        temp = rb.gravityScale;
        objectRenderer = GetComponent<Renderer>();
        isMaterialActive = false;
        canDash = true;
        timer = 0f;
    }

    // Update is called once per frame
    void Update() {

        if (rb.velocity.x > xLim) { //X velocity limit
            rb.velocity = new Vector2(rb.velocity.y, xLim);
        }

        if (Input.GetMouseButtonDown(1)) { //Respwn on RMB
            player.transform.position = spawn.position;
        }

        
        if (Input.GetMouseButton(0) && rb.drag < 10) { //Slow on touch
            rb.drag += Time.deltaTime * slowRate;
            rb.gravityScale = 0;
            boost += Time.deltaTime * boostRate;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        } else if (Input.GetMouseButtonUp(0)) {
            rb.drag = 0;
            rb.gravityScale = temp;
            rb.velocity = new Vector2(0.0f, boost);
            boost = 0;
            rb.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        }

        //Swipe controls
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began) {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended) {
            endTouchPosition = Input.GetTouch(0).position;
            if (Vector2.Distance(endTouchPosition, startTouchPosition) > min && canDash == true) {
                direction = (endTouchPosition - startTouchPosition).normalized;
                //canDash = false; 
                rb.velocity = direction * speed;
                DOVirtual.Float(x, y, z, RigidbodyDrag);                                           //HERE!!!
                //StartCoroutine(dashWait());
            }
        }

        //Change player color when they're moving fast enough (to show they can destroy enemies)
        if (rb.velocity.magnitude > E.threshold) {
            objectRenderer.material = newMaterial;
            isMaterialActive = true;
        }
        if (isMaterialActive) {
            timer += Time.deltaTime;
            if (timer >= changeDuration) {
                objectRenderer.material = null;
                isMaterialActive = false;
                timer = 0f;
            }
        }

        //update gravityScale as you gain height
        //rb.gravityScale = player.transform.position.y;
    }


   /* IEnumerator dashWait() {
        yield return new WaitForSeconds(A);
        canDash = true;
    }*/

    void RigidbodyDrag(float x) {
        rb.drag = x;
    }

    /*void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Wall")) {
            if (rb.velocity.magnitude < wallLim && rb.velocity.y > 0) {
                Debug.Log("YEPERS");
                rb.velocity = new Vector2(rb.velocity.y + wallBoost, rb.velocity.x);
            }
        }
    }*/

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Hazzard") || collision.gameObject.CompareTag("HazzardActivate")) {
            player.transform.position = spawn.position;
        }
        if (collision.gameObject.CompareTag("Wall") && rb.velocity.y > 0) {
            //StartCoroutine(gravityOnOff());
            rb.gravityScale = 0f;
            DOVirtual.Float(0, temp, .5f, UpdateGravity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        /*if (collision.CompareTag("LSD")) {
            //spline = GetComponent<>();
        }*/
    }

    /*IEnumerator gravityOnOff() {
        rb.gravityScale = 0f;
        //yield return new WaitForSeconds(A);
        DOVirtual.Float(0, temp, .5f, UpdateGravity);
        //rb.gravityScale = temp;
    }*/
    void UpdateGravity(float value) {
        rb.gravityScale = value;
    }

    /*
    void LSD() {

    }*/
}
