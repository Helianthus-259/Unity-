using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text scoreText;
    private int score;

    public DiskConfig diskConfig; // ����DiskConfig��Դ

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
        score += diskscore; // �������ӵ���ǰ�÷���
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

