using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDestroyable, IGameStateListener
{
    [Header("Configs")]
    [SerializeField] 
    private GameManager m_gameManager;

    [Header("Components")] 
    [SerializeField]
    private SpriteRenderer m_renderer;
    [SerializeField]
    private PlayerMovementController m_movementController;

    [SerializeField] 
    private ShooterController m_shooterController;
    
    [SerializeField] 
    private ParticleSystem m_destroyParticlesPrefab;

    
    private bool m_playerInvulnerable;

    private void Awake()
    {
        gameObject.SetActive(false);
        SubscribeToGameState();
    }

    private void OnDestroy()
    {
        UnSubscribeToGameState();
    }

    private void OnGameStart()
    {
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
        m_playerInvulnerable = false;
        m_movementController.SetBaseSpeed(m_gameManager.GameConfig.PlayerSpeed);
        m_shooterController.SetShootCooldown(m_gameManager.GameConfig.PlayerShootCooldown);
    }

    public void TakeHit()
    {
        if (m_playerInvulnerable)
        {
            return;
        }
        
        EventManager.Send(EventManager.EventTypes.PlayerHit);
        
        if (m_gameManager.GameStats.CurrentLives<=0)
        {
            OnDestroyed();
        }
        else
        {
            StartCoroutine(BlinkSprite(m_gameManager.GameConfig.PlayerInvulnerableTime,3));
            StartCoroutine(InvulnerableCoroutine());
        }
    }

    public void OnDestroyed()
    {
        GameObject newParticles = GameObjectPoolManager.New(m_destroyParticlesPrefab.gameObject);
        newParticles.transform.position = this.transform.position;
        
        EventManager.Send(EventManager.EventTypes.PlayerDestroyed);
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    private IEnumerator InvulnerableCoroutine()
    {
        m_playerInvulnerable = false;
        yield return new WaitForSeconds(m_gameManager.GameConfig.PlayerInvulnerableTime);
        m_playerInvulnerable = false;
    }

    private IEnumerator BlinkSprite(float duration, int loops)
    {
        Color color = m_renderer.color;
        float fadeDuration = duration / (loops * 2);
        for(int i=0;i<loops;i++)
        {
            yield return StartCoroutine(Fade(1, 0, fadeDuration));
            yield return StartCoroutine(Fade(0, 1, fadeDuration));
        }
        color.a = 1;
        m_renderer.color = color;
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float timer = 0;
        Color color = m_renderer.color;
        while (timer < duration)
        {
            color.a = Mathf.Lerp(from, to, timer / duration);
            m_renderer.color = color;
            timer += Time.deltaTime;
            yield return null;
        }
        color.a = to;
        m_renderer.color = color;
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
                break;
        }
    }
}
