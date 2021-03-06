using UnityEngine;

public class PlayerShooterController : ShooterController
{
    void Update()
    {
        if (GameManager.GameState != GameManager.GameStates.Started)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.realtimeSinceStartup >= (m_lastShotTimestamp + m_shootCooldownSeconds))
            {
                Shoot();
            }
        }
    }
}
