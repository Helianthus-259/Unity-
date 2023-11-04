using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text scoreText;
    private int score;

    public DiskConfig diskConfig; // 引用DiskConfig资源

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    public void IncreaseScore(int diskscore)
    {
        score += diskscore; // 将分数加到当前得分上
        UpdateScoreText();
        if (score >= 50)
        {
            score = 0;
            UpdateScoreText();
            GameManager.Instance.EndGame("You Win!");
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}

