using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        NotStarted,
        Started,
        GameOver
    }
     
    [Header("Configs")]
    [SerializeField] 
    private GameConfig m_gameConfig;
    [SerializeField] 
    private EnemyConfig m_enemyConfig;
    [SerializeField] 
    private GameStats m_gameStats;

    public GameConfig GameConfig=> m_gameConfig;
    public GameStats GameStats => m_gameStats;

    public static GameStates GameState { get; private set; }

    private void Start()
    {
        m_gameStats.Init();
        EventManager.Subscribe(EventManager.EventTypes.PlayerHit, OnPlayerHit);
        EventManager.Subscribe<EventManager.EnemyDestroyedEvent>(OnEnemyDestroyed);
        EventManager.Subscribe(EventManager.EventTypes.EnemyReachedBottom, OnEnemyReachedBottom);
        EventManager.Subscribe(EventManager.EventTypes.AllEnemiesDestroyed, OnAllEnemiesDestroyed);
    }


    private void OnDestroy()
    {
        EventManager.UnSubscribe(EventManager.EventTypes.PlayerHit, OnPlayerHit);
        EventManager.UnSubscribe<EventManager.EnemyDestroyedEvent>(OnEnemyDestroyed);
        EventManager.UnSubscribe(EventManager.EventTypes.EnemyReachedBottom, OnEnemyReachedBottom);
        EventManager.UnSubscribe(EventManager.EventTypes.AllEnemiesDestroyed, OnAllEnemiesDestroyed);
    }

    private void OnAllEnemiesDestroyed()
    {
        SetGameState(GameStates.GameOver);
    }


    private void OnEnemyDestroyed(EventManager.EnemyDestroyedEvent data)
    {
        int rewardScore = m_enemyConfig.GetEnemyScore(data.EnemyDestroyed.EnemyType);
        m_gameStats.AddScore(rewardScore);
    }

    private void OnPlayerHit()
    {
        GameStats.UpdateLives(-1);
        if (GameStats.CurrentLives <= 0)
        {
            SetGameState(GameStates.GameOver);
        }
    }
    
    private void OnEnemyReachedBottom()
    {
        GameStats.UpdateLives(-GameStats.CurrentLives);
        SetGameState(GameStates.GameOver);
    }

    
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && GameState!=GameStates.Started)
        {
            StartGame();
        }
    }


    private void StartGame()
    {
        SetGameState(GameStates.Started);
    }
    

    private void SetGameState(GameStates newState)
    {
        GameState = newState;
        EventManager.Send(EventManager.EventTypes.GameStateChanged);
    }
}
