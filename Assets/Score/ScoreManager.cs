using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    private int score;
    private int maxScore;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + score + "/" + maxScore;
    }

    public void SetMaxScore(int value)
    {
        maxScore = value;
        UpdateUI();
    }    

    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
    }
}
