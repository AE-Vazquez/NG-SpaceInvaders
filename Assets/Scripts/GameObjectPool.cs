using System.Collections.Generic;
using UnityEngine;

class GameObjectPool
{
    public GameObject prefab = null;
    public Queue<PooledGameObject> instances = null;

    Transform m_parent = null;

    public GameObjectPool(Transform parent, GameObject prefab, int capacity, int numInstances)
    {
        this.prefab = prefab;
        instances = new Queue<PooledGameObject>(Mathf.Max(capacity, numInstances));
        m_parent = parent;

        for (int i = 0; i < numInstances; ++i)
        {
            PooledGameObject instance = CreateInstance();
            instance.SetPooled(true);
            instances.Enqueue(instance);
        }
    }

    PooledGameObject CreateInstance()
    {
        GameObject instance = GameObject.Instantiate(prefab, m_parent);

        PooledGameObject pooled = instance.GetComponent<PooledGameObject>() ?? instance.AddComponent<PooledGameObject>();
        pooled.PoolId = prefab.GetInstanceID();
			
        return pooled;
    }
    

    public GameObject New()
    {
        PooledGameObject instance = null;

        if (instances.Count > 0)
        {
            instance = instances.Dequeue();
        }
        else
        {
            instance = CreateInstance();
        }

        instance.SetPooled(false);
        return instance.gameObject;
    }

    public void Dispose(PooledGameObject instance)
    {
        instance.SetPooled(true);

        /*Transform trans = instance.transform;
        Vector3 pos = trans.localPosition;
        Vector3 scale = trans.localScale;
        Quaternion rotation = trans.localRotation;

        trans.SetParent(m_parent);
        trans.localPosition = pos;
        trans.localScale = scale;
        trans.localRotation = rotation;*/

        instances.Enqueue(instance);
    }
}