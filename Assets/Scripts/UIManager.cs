using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Game Control Buttons")]
    [SerializeField] private GameObject mobileControlButtons;
    [SerializeField] private GameObject pauseButton;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject pauseUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        ShowGameplayUI();
    }

    void Update()
    {
        
    }

    public void ShowGameplayUI()
    {
        if (mobileControlButtons != null)
            mobileControlButtons.SetActive(true);
        
        if (pauseButton != null)
            pauseButton.SetActive(true);

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
        
        if (winUI != null)
            winUI.SetActive(false);
        
        if (pauseUI != null)
            pauseUI.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        if (mobileControlButtons != null)
            mobileControlButtons.SetActive(false);
        
        if (pauseButton != null)
            pauseButton.SetActive(false);

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    public void ShowWinUI()
    {
        if (mobileControlButtons != null)
            mobileControlButtons.SetActive(false);
        
        if (pauseButton != null)
            pauseButton.SetActive(false);

        if (winUI != null)
            winUI.SetActive(true);
    }

    public void ShowPauseUI()
    {
        if (mobileControlButtons != null)
            mobileControlButtons.SetActive(false);

        if (pauseButton != null)
            pauseButton.SetActive(false);

        if (pauseUI != null)
            pauseUI.SetActive(true);
    }

    public void HidePauseUI()
    {
        if (mobileControlButtons != null)
            mobileControlButtons.SetActive(true);

        if (pauseButton != null)
            pauseButton.SetActive(true);

        if (pauseUI != null)
            pauseUI.SetActive(false);
    }
}
