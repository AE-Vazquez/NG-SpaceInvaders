using UnityEngine;

public class PooledGameObject : MonoBehaviour, IPoolable
{
    #region IPoolable
    public int PoolId { get; set; }

    public void SetPooled(bool pooled)
    {
        gameObject.SetActive(!pooled);
    }

    public void Dispose()
    {
        GameObjectPoolManager.Dispose(gameObject);
    }
    
    #endregion
    
}
