using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI winScoreText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject pauseUI;
    private bool isGameOver = false;
    private bool isWin = false;
    private bool isPaused = false;

    void Start()
    {
        UpdateScore();
        gameOverUI.SetActive(false);
        if (winUI != null)
        {
            winUI.SetActive(false); 
        }
        if (pauseUI != null)
        {
            pauseUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void AddScore(int point)
    {
        if (isGameOver || isWin) return;
        score += point;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void GameOver(float delay = 2f)
    {
        if (isGameOver) return;
        isGameOver = true;
        StartCoroutine(GameOverDelayed(delay));
    }

    private IEnumerator GameOverDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        score = 0;
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public void Win(float delay = 1f)
    {
        if (isWin || isGameOver) return;
        isWin = true;
        StartCoroutine(WinDelayed(delay));
    }

    private IEnumerator WinDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
        if (winUI != null)
        {
            winUI.SetActive(true);
            UpdateWinScore();
        }
        else
        {
            Debug.LogWarning("Win UI is not assigned!");
        }
    }

    private void UpdateWinScore()
    {
        if (winScoreText != null)
        {
            winScoreText.text = "Score: " + score.ToString();
        }
    }

    public void Pause()
    {
        if (isGameOver || isWin) return;
        isPaused = true;
        Time.timeScale = 0f;
        if (pauseUI != null)
        {
            pauseUI.SetActive(true);
        }
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseUI != null)
        {
            pauseUI.SetActive(false);
        }
    }

    public void RestartGame()
    {
        isGameOver = false;
        isWin = false;
        isPaused = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        isGameOver = false;
        isWin = false;
        isPaused = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void NextGame()
    {
        isGameOver = false;
        isWin = false;
        isPaused = false;
        score = 0;
        Time.timeScale = 1f;
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("This is the last level!");
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Restart()
    {
        RestartGame();
    }
}
