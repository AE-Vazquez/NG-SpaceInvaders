using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDestroyable
{
    [Header("Configs")]
    [SerializeField] 
    private GameConfig m_gameConfig;

    [Header("Components")] 
    [SerializeField]
    private SpriteRenderer m_renderer;
    [SerializeField]
    private PlayerMovementController m_movementController;

    [SerializeField] 
    private ShooterController m_shooterController;
    
    private int m_currentHitPoints;
    private bool m_playerInvulnerable;

    public void Start()
    {
        m_currentHitPoints = m_gameConfig.PlayerLives;
        m_movementController.SetBaseSpeed(m_gameConfig.PlayerSpeed);
        m_shooterController.SetShootCooldown(m_gameConfig.PlayerShootCooldown);
    }

    public void TakeHit()
    {
        if (m_playerInvulnerable)
        {
            return;
        }
        
        EventManager.Send(EventManager.EventTypes.PlayerHit);

        m_currentHitPoints--;
        if (m_currentHitPoints<=0)
        {
            OnDestroyed();
        }
        else
        {
            StartCoroutine(BlinkSprite(m_gameConfig.PlayerInvulnerableTime,3));
            StartCoroutine(InvulnerableCoroutine());
        }
    }

    public void OnDestroyed()
    {
        EventManager.Send(EventManager.EventTypes.PlayerDestroyed);
        Destroy(gameObject);
    }

    private IEnumerator InvulnerableCoroutine()
    {
        m_playerInvulnerable = false;
        yield return new WaitForSeconds(m_gameConfig.PlayerInvulnerableTime);
        m_playerInvulnerable = false;
    }

    private IEnumerator BlinkSprite(float duration, int loops)
    {
        float timer = 0;
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
}
