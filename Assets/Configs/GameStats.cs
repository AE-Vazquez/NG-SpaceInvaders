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
        EventManager.Send(EventManager.EventTypes.ScoreChanged);
    }

    private void SetScore(int score)
    {
        m_currentScore = score;
        EventManager.Send(EventManager.EventTypes.ScoreChanged);
    }

    public void UpdateLives(int amount)
    {
        m_currentLives += amount;
        EventManager.Send(EventManager.EventTypes.LivesChanged);
    }

    private void SetLives(int amount)
    {
        m_currentLives = amount;
        EventManager.Send(EventManager.EventTypes.LivesChanged);
    }

    void OnGameStart()
    {
        SetLives(m_gameConfig.PlayerLives);
        SetScore(0);
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
