using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    protected float m_baseSpeed=1f;

    [Header("Components")]
    [SerializeField]
    protected Rigidbody2D m_rigidBody;

    protected float m_currentSpeed;

    public void SetBaseSpeed(float baseSpeed)
    {
        m_baseSpeed = baseSpeed;
    }
    
    protected void SetSpeed(float newSpeed)
    {
        m_rigidBody.velocity = Vector2.left * newSpeed;
        m_currentSpeed = newSpeed;
    }
    
}