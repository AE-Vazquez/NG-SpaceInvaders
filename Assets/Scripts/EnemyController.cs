using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] 
    private GameConfig m_gameConfig;
    [SerializeField] 
    private EnemyConfig m_enemyConfig;
    

    [SerializeField] 
    private Transform m_enemyContainer;
    
    private List<Enemy> spawnedEnemies;

    private float m_enemyStep;
    private float m_speedFactor;

    private float leftBound;
    private float rightBound;

    void Start()
    {
        rightBound =  Camera.main.orthographicSize * Screen.width / Screen.height;
        leftBound = -rightBound;
        m_enemyStep = 1;
        m_speedFactor = m_gameConfig.EnemyBaseSpeed;

        spawnedEnemies = new List<Enemy>();
        SpawnEnemies();
        
        StartCoroutine(MoveEnemies());

        EventManager.Connect<EventManager.EnemyDestroyedEvent>(OnEnemyDestroyed);
    }

    private void OnEnemyDestroyed(EventManager.EnemyDestroyedEvent data)
    {
        spawnedEnemies.Remove(data.EnemyDestroyed);
    }
    
    private void SpawnEnemies()
    {
        Vector2 position=Vector2.zero;
        foreach (GameConfig.EnemyRow row in m_gameConfig.EnemyRows)
        {
            GameObject enemyPrefab = m_enemyConfig.GetEnemyPrefab(row.EnemyType);
            if (enemyPrefab == null)
            {
                Debug.LogError($"No Prefab found for enemy {row.EnemyType}, skipping row");
                continue;
            }
            for (int i = 0; i < row.EnemyCount; i++)
            {
                Enemy newEnemy = Instantiate(enemyPrefab,transform).GetComponent<Enemy>();
                spawnedEnemies.Add(newEnemy);
                newEnemy.transform.localPosition = position;
                position.x += 1;
            }
            position.x = 0;
            position.y -= 1;
        }
    }

    private IEnumerator MoveEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_speedFactor);
            m_speedFactor *= m_gameConfig.DifficultyScale;
            
            //If any enemy would go out of bounds on next step, move the whole group down and reverse the direction
            if(CheckEnemiesOutOfBounds())
            {
                m_enemyContainer.Translate(Vector3.down);
                m_enemyStep *= -1;
            }
            else
            {
                foreach (var enemyTransform in spawnedEnemies.Select(enemy => enemy.transform))
                {
                    enemyTransform.Translate(Vector3.right * m_enemyStep);
                }
            }
        }
    }

    private bool CheckEnemiesOutOfBounds()
    {
        return spawnedEnemies.Select(enemy => enemy.transform).Any(enemyTransform =>
            enemyTransform.position.x + m_enemyStep >= rightBound ||
            enemyTransform.position.x + m_enemyStep <= leftBound);
    }
}
