using UnityEngine;

public class PooledGameObject : MonoBehaviour
{
    public int PoolId { get; set; }

    public void SetPooled(bool pooled)
    {
        gameObject.SetActive(!pooled);
    }
}
