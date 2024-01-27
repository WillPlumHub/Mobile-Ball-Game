using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class chest : MonoBehaviour {

    public int hp;
    public int score = 5;
    public int spawnNum;
    public GameObject coin;

    public float radius = 3.0f;

    public movement mov;
    public scoringManager S;

    public void Start() {
        GameObject scoreManagerObject = GameObject.Find("ScoreManager");
        
        if (scoreManagerObject != null) {
            //Debug.Log("Worked" + scoreManagerObject.name);
            S = scoreManagerObject.GetComponent<scoringManager>();
            //Debug.Log("Worked" + S.name);
        }
        GameObject movementFinder = GameObject.Find("Player");
        if (movementFinder != null) {
            mov = movementFinder.GetComponent<movement>();
            if (mov == null) {
                Debug.LogError("scoringManager component not found on the GameObject named Scoremanager");
            } else {
                Debug.LogError("GameObject named Scoremanager not found in the scene.");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && mov.isMaterialActive) {
            coinSpawn(1); 
            hp--;
        }
    }

    private void Update() {
        if (hp == 0 && coin != null) {
            coinSpawn(spawnNum);
            Destroy(gameObject);
        }
    }

    void coinSpawn(int spawnNum) {
        for (int i = spawnNum; i > 0; i--) {
            float angle = i * (2 * Mathf.PI) / spawnNum; //calc the angle for each coin
            float x = transform.position.x + radius * Mathf.Cos(angle); //calc the position in a circle w/ polar coordinates
            float y = transform.position.y + radius * Mathf.Sin(angle);
            Vector2 spawnPos = new Vector2(x, y); //create a Vector3 w/ the calc'd position and the original z-coordinate
            Instantiate(coin, spawnPos, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && gameObject != null) {
            S.IncreaseScore(score);
            Destroy(gameObject);
        }
    }
}