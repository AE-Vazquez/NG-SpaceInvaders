public class EnemyMovementController : MovementController
{
    private void Start()
    {
        SetSpeed(m_moveSpeed);
    }

    private void ChangeDirection()
    {
        SetSpeed(m_currentSpeed*-1);
    }
    
}
