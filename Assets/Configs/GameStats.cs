using UnityEngine;

[CreateAssetMenu(menuName = "Game Stats")]
public class GameStats : ScriptableObject , IGameStateListener
{
    private const string MaxScorePrefsKey = "MaxScore";

    [SerializeField] 
    private GameConfig m_gameConfig;

    private int m_currentScore;
    private int m_currentLives;

    public int CurrentScore => m_currentScore;
    public int CurrentLives => m_currentLives;

    public void Init()
    {
        SubscribeToGameState();
    }

    public void AddScore(int score)
    {
        m_currentScore += score;
    }

    public void UpdateLives(int amount)
    {
        m_currentLives += amount;
    }

    void OnGameStart()
    {
        m_currentLives = m_gameConfig.PlayerLives;
        m_currentScore = 0;
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
        }
    }
}
