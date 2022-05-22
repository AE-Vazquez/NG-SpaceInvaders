using System;
using System.Net;
using UnityEngine;

public class Projectile : PooledGameObject
{
    public enum ProjectileTypes
    {
        Player,
        Enemy
    }
    [Header("Components")] 
    [SerializeField]
    private Rigidbody2D m_rigidBody;
    
    private ProjectileTypes m_projectileType;
    public void SetProjectileType(ProjectileTypes projectileType)
    {
        m_projectileType = projectileType;
    }
    public void SetSpeed(float newSpeed)
    {
        m_rigidBody.velocity = Vector2.up * newSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //No friendly fire
        if (other.CompareTag(gameObject.tag) ||
            m_projectileType == ProjectileTypes.Player && other.CompareTag("Player") ||
            (m_projectileType == ProjectileTypes.Enemy && other.CompareTag("Enemy")))
        {
            return;
        }
        
        IDestroyable target = other.gameObject.GetComponent<IDestroyable>();
        target?.TakeHit();

        Dispose();
    }
}
