using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour, IGameStateListener
{
    [Header("Configs")] 
    [SerializeField] private GameStats m_gameStats;
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private Transform m_livesContainer;
    [SerializeField] private GameObject m_startGameScreen;
    [SerializeField] private GameObject m_gameOverScreen;
    [SerializeField] private TextMeshProUGUI m_gameOverScore;
    [SerializeField] private GameObject m_gameOverWin;
    [SerializeField] private GameObject m_gameOverLose;

    private void Start()
    {
        SubscribeToGameState();
        EventManager.Subscribe(EventManager.EventTypes.ScoreChanged, OnScoreChanged);
        EventManager.Subscribe(EventManager.EventTypes.LivesChanged, OnLivesChanged);
        
        m_startGameScreen.SetActive(true);
        m_gameOverScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        UnSubscribeToGameState();
    }

    private void OnGameStart()
    {
        m_startGameScreen.gameObject.SetActive(false);
        m_gameOverScreen.gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        m_gameOverScreen.gameObject.SetActive(true);
        m_gameOverScore.text = m_gameStats.CurrentScore.ToString("000");
        m_gameOverWin.SetActive(m_gameStats.CurrentLives>0);
        m_gameOverLose.SetActive(m_gameStats.CurrentLives<=00);
    }


    private void OnScoreChanged()
    {
        m_scoreText.text = m_gameStats.CurrentScore.ToString("000");
    }

    private void OnLivesChanged()
    {
        int currentLives = m_gameStats.CurrentLives;
        for (int i = 0; i < m_livesContainer.childCount; i++)
        {
            m_livesContainer.GetChild(i).gameObject.SetActive(i<currentLives);
        }
    }
    
    public void SubscribeToGameState()
    {
        EventManager.Subscribe(EventManager.EventTypes.GameStateChanged, OnGameStateChanged);
    }

    public void UnSubscribeToGameState()
    {
        EventManager.UnSubscribe(EventManager.EventTypes.GameStateChanged, OnGameStateChanged);
    }

    public void OnGameStateChanged()
    {
        switch (GameManager.GameState)
        {
            case GameManager.GameStates.Started:
                OnGameStart();
                break;
            case GameManager.GameStates.GameOver:
                OnGameOver();
                break;
        }
    }
}
