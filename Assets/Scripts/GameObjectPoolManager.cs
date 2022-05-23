using UnityEngine;
using System.Collections.Generic;

public class GameObjectPoolManager : MonoBehaviour, IGameStateListener
{
	private static GameObjectPoolManager s_instance = null;
	
	Dictionary<int, GameObjectPool> m_pools = null;
	
	
	public static GameObject New(GameObject prefab)
	{
		GameObjectPool gameObjectPool = s_instance.GetPool(prefab) ?? s_instance.CreatePool(prefab);
		return gameObjectPool.New();
	}

	public static void Dispose(GameObject instance)
	{
		if (instance == null)
		{
			return;
		}
		
		if (s_instance != null)
		{
			PooledGameObject pooled = instance.GetComponent<PooledGameObject>();
			if (pooled == null)
			{
				Debug.LogWarning("Trying to dispose a non-pooled object: " + instance.name);
				Destroy(instance);
				return;
			}

			GameObjectPool gameObjectPool = null;
			bool found = s_instance.m_pools.TryGetValue(pooled.PoolId, out gameObjectPool);

			if (found)
			{
				gameObjectPool.Dispose(pooled);
			}
			else
			{
				Destroy(instance);
			}
		}
		else
		{
			Destroy(instance);
		}
	}

	protected void Awake()
	{
		if (s_instance != null)
		{
			Destroy(this);
			return;
		}
		s_instance = this;
		
		m_pools = new Dictionary<int, GameObjectPool>();
		SubscribeToGameState();
	}

	protected void OnDestroy()
	{
		if (s_instance == this)
		{
			s_instance = null;
		}
		
		UnSubscribeToGameState();
	}

	private GameObjectPool GetPool(GameObject prefab)
	{
		int id = prefab.GetInstanceID();

		return m_pools.TryGetValue(id, out var gameObjectPool) ? gameObjectPool : null;
	}

	private GameObjectPool CreatePool(GameObject prefab, int numInstances = 0)
	{
		int id = prefab.GetInstanceID();
		GameObjectPool pool = new GameObjectPool(transform, prefab, Mathf.Max(10, numInstances), numInstances);
		m_pools.Add(id, pool);

		return pool;
	}

	private void CleanPooledObjects()
	{
		foreach (Transform child in transform)
		{
			Dispose(child.gameObject);
		}
	}

	public void SubscribeToGameState()
	{
		EventManager.Subscribe(EventManager.EventTypes.GameStateChanged, OnGameStateChanged);
	}

	public void UnSubscribeToGameState()
	{
		EventManager.UnSubscribe(EventManager.EventTypes.GameStateChanged, OnGameStateChanged);
	}

	public void OnGameStateChanged()
	{
		switch (GameManager.GameState)
		{
			case GameManager.GameStates.Started:
				CleanPooledObjects();
				break;
			case GameManager.GameStates.GameOver:
				CleanPooledObjects();
				break;
		}
	}
}