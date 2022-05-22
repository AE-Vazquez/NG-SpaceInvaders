using System;
using System.Collections.Generic;

public class EventManager
{

    public enum EventTypes
    {
        PlayerHit,
        PlayerDestroyed
    }
    public delegate void OnGameEvent();
    
    public delegate void OnGameEvent<T>(T data) where T : GameEvent;
    
    #region GameEvents definitions
    public interface GameEvent
    {
    }
    
    public class EnemyDestroyedEvent : GameEvent
    {
        public Enemy EnemyDestroyed;
    }
    
    #endregion

    
    #region Game Events by class (  With data )
    private class GameEventHandler<T> where T : GameEvent
    {
        public static OnGameEvent<T> handler;
    }

    public static void Connect<T>(OnGameEvent<T> handler) where T : GameEvent
    {
        GameEventHandler<T>.handler += handler;
    }

    public static void Disconnect<T>(OnGameEvent<T> handler) where T : GameEvent
    {
        GameEventHandler<T>.handler -= handler;
    }

    public static void Send<T>(T data) where T : GameEvent
    {
        OnGameEvent<T> handler = GameEventHandler<T>.handler;
        handler?.Invoke(data);
    }
    
    #endregion 
    
    #region Events by Type ( No extra data )
    private static Dictionary<EventTypes, OnGameEvent> s_subscribers = new Dictionary<EventTypes, OnGameEvent>();
    
    
    public static void Subscribe(EventTypes eventType, OnGameEvent handler)
    {
        if (handler == null) { throw new ArgumentNullException(); }

        if (s_subscribers.ContainsKey(eventType))
        {
            s_subscribers[eventType] += handler;
            return;
        }
        OnGameEvent action = null;
        action += handler;
        s_subscribers.Add(eventType, action);
    }


    public static void Unsubscribe(EventTypes eventType, OnGameEvent handler)
    {
        if (s_subscribers.ContainsKey(eventType))
        {
            s_subscribers[eventType] -= handler;
            if (s_subscribers[eventType] == null) { s_subscribers.Remove(eventType); }
        }
    }


    public static void Send(EventTypes eventType)
    {
        OnGameEvent action = null;
        if (s_subscribers.TryGetValue(eventType, out action))
        {
            action();
        }
    }
    
    #endregion
}