using System.Collections;
using UnityEngine;

public class AutoShooterController : ShooterController
{
    void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (GameManager.GameState==GameManager.GameStates.Started)
        {
            yield return new WaitForSeconds(m_shootCooldownSeconds + Random.Range(0, m_shootCooldownSeconds * 0.5f));
            Shoot();
        }
    }
}
