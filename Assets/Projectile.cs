
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField]
    private Rigidbody2D m_rigidBody;
    
    public void SetSpeed(float newSpeed)
    {
        m_rigidBody.velocity = Vector2.up * newSpeed;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
    
    
}