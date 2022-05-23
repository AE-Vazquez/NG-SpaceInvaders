using UnityEngine;

public class Enemy : MonoBehaviour, IDestroyable
{
    [Header("Components")] 
    [SerializeField]
    private ShooterController m_shooterController;
    
    [SerializeField] 
    private ParticleSystem m_destroyParticlesPrefab;
    
    
    private EnemyConfig.EnemyTypes m_enemyType;
    public  EnemyConfig.EnemyTypes EnemyType=>m_enemyType;

    public void Init(EnemyConfig.EnemyTypes enemyType)
    {
        m_enemyType = enemyType;
    }

    public void Shoot()
    {
        m_shooterController.Shoot();
    }

    public void TakeHit()
    {
        OnDestroyed();
    }

    public void OnDestroyed()
    {
        GameObject newParticles = GameObjectPoolManager.New(m_destroyParticlesPrefab.gameObject);
        newParticles.transform.position = this.transform.position;

        EventManager.Send(new EventManager.EnemyDestroyedEvent
        {
            EnemyDestroyed = this
        });
        
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall") || other.collider.CompareTag("Player"))
        {
            EventManager.Send(EventManager.EventTypes.EnemyReachedBottom);
        }
    }
}
