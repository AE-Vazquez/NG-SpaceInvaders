using System.Collections.Generic;
using UnityEngine;

class GameObjectPool
{
    private GameObject m_prefab;
    private Queue<PooledGameObject> m_instances;

    private Transform m_parent;

    public GameObjectPool(Transform parent, GameObject prefab, int capacity, int numInstances)
    {
        m_prefab = prefab;
        m_instances = new Queue<PooledGameObject>(Mathf.Max(capacity, numInstances));
        m_parent = parent;

        for (int i = 0; i < numInstances; ++i)
        {
            PooledGameObject instance = CreateInstance();
            instance.SetPooled(true);
            m_instances.Enqueue(instance);
        }
    }

    PooledGameObject CreateInstance()
    {
        GameObject instance = GameObject.Instantiate(m_prefab, m_parent);

        PooledGameObject pooled = instance.GetComponent<PooledGameObject>() ?? instance.AddComponent<PooledGameObject>();
        pooled.PoolId = m_prefab.GetInstanceID();
			
        return pooled;
    }
    

    public GameObject New()
    {
        PooledGameObject instance  = m_instances.Count > 0 ? m_instances.Dequeue() : CreateInstance();

        instance.SetPooled(false);
        return instance.gameObject;
    }

    public void Dispose(PooledGameObject instance)
    {
        instance.SetPooled(true);
        m_instances.Enqueue(instance);
    }
}