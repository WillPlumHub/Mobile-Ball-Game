using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class camFollow : MonoBehaviour {

    public Transform player;
    public float smoothSpeed = 1.0f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (player != null) {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, player.position.z - 1);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}