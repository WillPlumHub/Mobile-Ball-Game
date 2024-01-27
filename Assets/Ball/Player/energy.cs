using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energy : MonoBehaviour {
    public float en;
    public float lim;
    public float threshold = 8.0f;
    public int score;

    void OnCollisionEnter2D(Collision2D col) {
        if (en < lim) {
            en++;
        }
    }
}
