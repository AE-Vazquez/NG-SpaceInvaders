using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    protected float m_shootCooldownSeconds=1f;
    
    [SerializeField]
    protected float m_projectileSpeed=1f;


    [Header("Components")]
    [SerializeField]
    protected Projectile m_projectilePrefab;
    
    protected float m_lastShotTimestamp;

    protected void Shoot()
    {
        Projectile newProjectile = GameObjectPoolManager.New(m_projectilePrefab.gameObject).GetComponent<Projectile>();
        newProjectile.SetSpeed(m_projectileSpeed);
        newProjectile.transform.position = this.transform.position;
        m_lastShotTimestamp = Time.realtimeSinceStartup;
    }
}