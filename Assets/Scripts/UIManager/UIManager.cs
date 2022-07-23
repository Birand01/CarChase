using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Text highScoreText, gameOverScoreText;
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private ParticleSystem deadParticle;
    private float timer;
    private int score;
    private int highScore;
  

    private bool isGameStart = false;

    public bool GameStarted { get { return isGameStart; } set { isGameStart = value; } }
    public int Score { get { return score; } set { score = value; } }
    private void OnEnable()
    {
        PlayerHealth.OnGameOverScreen += GameOverScreen;
    }
    protected override void Awake()
    {
        score = 0;
        scoreText.text = score.ToString();  
    }
    private void Start()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }
    private void Update()
    {
        ScoreCounter();
    }
    private void ScoreCounter()
    {
        if(GameStarted)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                score += 1 * scoreMultiplier;
                scoreText.text = score.ToString();
                timer = 0;
            }
        }
        
    }
    public void GameState(bool gameState)
    {
        score = 0;
        isGameStart = gameState;
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void GameOverScreen()
    {
        StartCoroutine(GetEndScreen());
    }
    
    private void OnDisable()
    {
        PlayerHealth.OnGameOverScreen -= GameOverScreen;
    }

    IEnumerator GetEndScreen()
    {
        deadParticle.Play();
        yield return new WaitForSeconds(3f);
        deadParticle.Stop();
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.EndScreen);
        if(highScore<score)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        gameOverScoreText.text = "SCORE " + score.ToString();
        highScoreText.text = "HIGH SCORE " + highScore.ToString();
    }
}
