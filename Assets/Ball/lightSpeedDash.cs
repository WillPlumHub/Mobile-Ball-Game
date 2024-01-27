using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSpeedDash : MonoBehaviour {

    public int currentIndex;
    public GameObject ogGO;
    public Transform player;
    public Rigidbody2D playerRB;
    public GameObject[] points;

    public bool move;


    public float interpolateAmount;

   /* public BezierSpline spline; // Reference to the spline component
    public float speed = 5f; // Movement speed
    private float progress = 0f; // Current position on the spline
    private bool isMoving = false;*/

    // Start is called before the first frame update
    void Start() {
        int children = transform.childCount;
        points = new GameObject[children];
        fillArray(children);
        move = false;
    }

    // Update is called once per frame
    void Update() {
        if (move == true)
        {
            moveToPoint();
        }
    }

    void fillArray(int childrenCount) {
        for (int x = 0; x < childrenCount; x++) {
            points[x] = transform.GetChild(x).gameObject;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            //isMoving = true;
            //splineMove();
            move = true;
            Debug.Log("YES");
        }
    }

    /*void splineMove() {
        playerRB = other.GetComponent<Rigidbody2D>();
        playerRB.isKinematic = true;
        progress += speed * Time.deltaTime;

        if (progress > 1f)
        {
            // You can add logic here to stop the movement or reset the progress
            progress = 0f;
            isMoving = false;
            return;
        }

        // Get the position on the spline
        Vector3 splinePosition = spline.GetPoint(progress);

        // Move the object to the spline position
        transform.position = splinePosition;
    }*/

    void moveToPoint() {

        interpolateAmount = (interpolateAmount + Time.deltaTime) % 1f;
        player.position = Vector3.Lerp(points[0].transform.position, points[1].transform.position, interpolateAmount);
        move = false;
        
        /*currentIndex++;
        if (currentIndex < points.Length) {
            player.transform.position = points[currentIndex].transform.position;
        }*/
    }
}
