using UnityEngine;

public class Enemy : MonoBehaviour, IDestroyable
{
    
    [SerializeField] 
    private EnemyConfig.EnemyTypes m_enemyType;
    
    [SerializeField] 
    private ParticleSystem m_destroyParticlesPrefab;

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
