using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreSystem : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] private Text scoreText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text T_gameOverBestScore; 
    [SerializeField] private Text T_gameOverScore; 
    [SerializeField] private Text T_gameOverTotalMoney;

    [Header("Buttons")]
    [SerializeField] private Button B_Pause;


    private int scoreInt;
    private int bestScore;
    public bool scoreIsUpdating = false;

    [Header("Money UI")]
    [SerializeField] private Text moneyText;

    public int moneyInt;

    [Header("Car References")]
    [SerializeField] private Rigidbody carRb;

    private float scoreUpdateTime = 0.2f;
    private float nextScoreUpdate;


    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        moneyInt = PlayerPrefs.GetInt("TotalMoney", 0);
        UpdateScoreText();
        UpdateMoneyText();
        gameOverPanel.SetActive(false); // Скрыть панель окончания игры при старте
    }

    private void Update()
    {
        if (Time.time >= nextScoreUpdate && carRb.velocity.magnitude > 0.1)
        {
            scoreIsUpdating = true;
            nextScoreUpdate = Time.time + scoreUpdateTime;
            scoreInt++;
            UpdateScoreText();

            if (scoreInt > bestScore)
            {
                bestScore = scoreInt;
                PlayerPrefs.SetInt("BestScore", bestScore);
            }
        }
    }

    public void GameOver()
    {
        T_gameOverScore.text = "Score: " + scoreInt;
        T_gameOverBestScore.text = "Best score: " + bestScore;
        T_gameOverTotalMoney.text = "Total MOney: " + moneyInt;

        gameOverPanel.SetActive(true); // Показать панель окончания игры
        B_Pause.gameObject.SetActive(false);
    }

    private void UpdateScoreText()
    {
        if (scoreIsUpdating) { scoreText.text = scoreInt.ToString("D4"); }
       
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "" + moneyInt;
    }

    public void AddMoney(int amount)
    {
        moneyInt += amount;
        UpdateMoneyText();
        SaveMoney();
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("TotalMoney", moneyInt);
    }
}
