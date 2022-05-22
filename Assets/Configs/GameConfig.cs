using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Config")]
public class GameConfig : ScriptableObject
{
    [Serializable]
    public struct EnemyRow
    {
        public EnemyConfig.EnemyTypes EnemyType;
        [Range(1,8)]
        public int EnemyCount;
    }
    
    [SerializeField] private int m_playerLives;
    [SerializeField] private float m_enemyBaseSpeed;
    [SerializeField] private float m_difficultyScale;

    [SerializeField] private List<EnemyRow> m_enemyRows;

    public int PlayerLives => m_playerLives;
    public float EnemyBaseSpeed => m_enemyBaseSpeed;
    public float DifficultyScale  => m_difficultyScale;

    public List<EnemyRow> EnemyRows => m_enemyRows;
}
