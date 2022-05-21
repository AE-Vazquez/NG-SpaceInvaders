using UnityEngine;

public class EnemyMovementController : MovementController
{
    private void Start()
    {
        SetSpeed(m_moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
            SetSpeed(m_currentSpeed*-1);
        }
    }
}
