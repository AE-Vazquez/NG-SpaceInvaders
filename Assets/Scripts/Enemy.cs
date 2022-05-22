using UnityEngine;

public class Enemy : MonoBehaviour, IDestroyable
{
    
    [SerializeField] 
    private EnemyConfig.EnemyTypes m_enemyType;

    [Header("Components")] 
    [SerializeField]
    private ShooterController m_shooterController;
    
    [SerializeField] 
    private ParticleSystem m_destroyParticlesPrefab;

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
    
}
