using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    
    public enum EnemyTypes
    {
        Type1,
        Type2,
        Type3
    }
    [Serializable]
    public class EnemyData
    {
        public EnemyTypes EnemyType;
        public int Score;
        public GameObject EnemyPrefab;
    }

    [SerializeField] 
    private List<EnemyData> m_enemiesData;

    public List<EnemyData> EnemiesData => m_enemiesData;

    public GameObject GetEnemyPrefab(EnemyTypes enemyType)
    {
        EnemyData enemyData = m_enemiesData.Find((enemy) => enemy.EnemyType == enemyType);
        
        if (enemyData == null)
        {
            Debug.LogError($"Enemy config for {enemyType} not found");
            return null;
        }
        return enemyData.EnemyPrefab;
    }
    
    public int GetEnemyScore(EnemyTypes enemyType)
    {
        EnemyData enemyData = m_enemiesData.Find((enemy) => enemy.EnemyType == enemyType);
        
        if (enemyData == null)
        {
            Debug.LogError($"Enemy config for {enemyType} not found");
            return 0;
        }
        return enemyData.Score;
    }

}
