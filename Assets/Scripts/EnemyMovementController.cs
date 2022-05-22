public class EnemyMovementController : MovementController
{
    private void Start()
    {
        SetSpeed(m_baseSpeed);
    }

    private void ChangeDirection()
    {
        SetSpeed(m_currentSpeed*-1);
    }
    
}
