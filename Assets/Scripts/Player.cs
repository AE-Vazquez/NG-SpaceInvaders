using UnityEngine;

public class Player : MonoBehaviour, IDestroyable
{
    [SerializeField] 
    private int m_maxHitPoints=3;

    public void Start()
    {
        m_currentHitPoints = m_maxHitPoints;
    }
    private int m_currentHitPoints;
    public void TakeHit()
    {
        EventManager.Send(EventManager.EventTypes.PlayerHit);

        m_currentHitPoints--;
        if (m_currentHitPoints<=0)
        {
            OnDestroyed();
        }
    }

    public void OnDestroyed()
    {
        EventManager.Send(EventManager.EventTypes.PlayerDestroyed);
        Destroy(gameObject);
    }
}
