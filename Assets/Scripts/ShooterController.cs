using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [Header("Parameters")] 
    [SerializeField]
    protected Projectile.ProjectileTypes m_projectileType;
    [SerializeField]
    protected float m_shootCooldownSeconds=1f;
    [SerializeField]
    protected float m_projectileSpeed=1f;


    [Header("Components")]
    [SerializeField]
    protected Projectile m_projectilePrefab;
    
    protected float m_lastShotTimestamp;

    public void SetShootCooldown(float shootCooldown)
    {
        m_shootCooldownSeconds = shootCooldown;
    }

    public void Shoot()
    {
        Projectile newProjectile = GameObjectPoolManager.New(m_projectilePrefab.gameObject).GetComponent<Projectile>();
        newProjectile.SetSpeed(m_projectileSpeed);
        newProjectile.SetProjectileType(m_projectileType);
        newProjectile.transform.position = this.transform.position;
        m_lastShotTimestamp = Time.realtimeSinceStartup;
    }
}