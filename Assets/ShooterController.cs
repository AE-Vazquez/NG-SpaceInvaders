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

    protected float lastShotTimestamp;


    protected void Shoot()
    {
        Projectile newProjectile = Instantiate(m_projectilePrefab,transform.position,Quaternion.identity) as Projectile;
        newProjectile.SetSpeed(m_projectileSpeed);

        lastShotTimestamp = Time.realtimeSinceStartup;
    }
}