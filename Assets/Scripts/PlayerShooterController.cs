using UnityEngine;

public class PlayerShooterController : ShooterController
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Time.realtimeSinceStartup >= (lastShotTimestamp + m_shootCooldownSeconds))
            {
                Shoot();
            }
        }
    }
}
