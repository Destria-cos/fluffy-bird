using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public Text bestScoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject getReadyText;

    private int score;
    private int bestScore;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (bestScoreText != null) bestScoreText.text = "Best: " + bestScore.ToString();

        if (playButton != null) playButton.SetActive(true);
        if (gameOver != null) gameOver.SetActive(false);
        if (getReadyText != null) getReadyText.SetActive(true);

        Pause();
    }

    public void Play()
    {
        score = 0;
        if (scoreText != null) scoreText.text = score.ToString();

        if (playButton != null) playButton.SetActive(false);
        if (gameOver != null) gameOver.SetActive(false);
        if (getReadyText != null) getReadyText.SetActive(false);

        Time.timeScale = 1f;
        if (player != null) player.enabled = true;

        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        if (player != null) player.enabled = false;
    }

    public void GameOver()
    {
        if (getReadyText != null) getReadyText.SetActive(false);

        if (gameOver != null) gameOver.SetActive(true);
        if (playButton != null) playButton.SetActive(true);

        // Update highscore
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        if (bestScoreText != null) bestScoreText.text = "Best: " + bestScore.ToString();

        Pause();
    }

    public void IncrementScore()
    {
        score++;
        if (scoreText != null) scoreText.text = score.ToString();
    }
}
