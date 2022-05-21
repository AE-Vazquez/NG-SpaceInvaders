using System.Collections;
using UnityEngine;

public class EnemyShooterController : ShooterController
{
    void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_shootCooldownSeconds + Random.Range(0, m_shootCooldownSeconds * 0.5f));
            Shoot();
        }
    }
}
