using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoringManager : MonoBehaviour {

    public int score = 0;
    public Text scoreText;
    public TextMeshProUGUI scoreUI;

    // This method can be called whenever you want to increase the score
    public void IncreaseScore(int amount) {
        score += amount;
        UpdateScoreUI();
    }

    public void UpdateScoreUI() {
        scoreUI.text = score.ToString();
    }
}
