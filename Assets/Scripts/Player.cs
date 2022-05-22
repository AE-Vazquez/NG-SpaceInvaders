using UnityEngine;

public class Player : MonoBehaviour, IDestroyable
{
    [Header("Configs")]
    [SerializeField] 
    private GameConfig m_gameConfig;

    [Header("Components")] 
    [SerializeField]
    private PlayerMovementController m_movementController;

    [SerializeField] 
    private ShooterController m_shooterController;
    
    private int m_currentHitPoints;

    public void Start()
    {
        m_currentHitPoints = m_gameConfig.PlayerLives;
        m_movementController.SetBaseSpeed(m_gameConfig.PlayerSpeed);
        m_shooterController.SetShootCooldown(m_gameConfig.PlayerShootCooldown);
    }

    public void TakeHit()
    {
        EventManager.Send(EventManager.EventTypes.PlayerHit);

        m_currentHitPoints--;
        if (m_currentHitPoints<=0)
        {
            OnDestroyed();
        }
    }

    public void OnDestroyed()
    {
        EventManager.Send(EventManager.EventTypes.PlayerDestroyed);
        Destroy(gameObject);
    }
}
