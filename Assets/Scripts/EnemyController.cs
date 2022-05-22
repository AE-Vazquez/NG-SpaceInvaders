using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public enum EnemyTypes
    {
        Type1,
        Type2
    }
    
    [Serializable]
    public struct EnemyRow
    {
        public EnemyTypes EnemyType;
        [Range(1,8)]
        public int EnemyCount;
    }

    [SerializeField] 
    private float m_speedFactor;

    [SerializeField] 
    private float m_difficultyMultiplier=1;
    [SerializeField] 
    private float m_enemyStep;

    [SerializeField] 
    private Transform m_enemyContainer;
    [SerializeField] 
    private List<EnemyRow> m_enemyData;

    [SerializeField] 
    private Enemy m_enemyPrefab;
    
    private List<Enemy> spawnedEnemies;

    private float leftBound;
    private float rightBound;

    void Start()
    {
        rightBound =  Camera.main.orthographicSize * Screen.width / Screen.height;
        leftBound = -rightBound;

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
        foreach (EnemyRow row in m_enemyData)
        {
            for (int i = 0; i < row.EnemyCount; i++)
            {
                Enemy newEnemy = Instantiate(m_enemyPrefab,transform);
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
            m_speedFactor *= m_difficultyMultiplier;
            
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
