using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazzard : MonoBehaviour {

    public float moveRate;
    public float activationDistance = 10f;
    public float waitTime;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null) {
            // Calculate the distance between the player and this GameObject
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Check if the player is close enough to activate the falling rocks
            if (distanceToPlayer < activationDistance) {
                ActivateFallingRocks();
            }
        }
    }

    void ActivateFallingRocks() {
        transform.Translate(Vector3.down * moveRate * Time.deltaTime);
        StartCoroutine(destroy(waitTime));
    }

    private IEnumerator destroy(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
