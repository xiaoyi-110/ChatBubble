using System;
using System.Collections.Generic;
using UnityEngine;
public class EventManager : MonoSingleton<EventManager>
{
    private Dictionary<int, EventHandler> m_EventHandlers = new Dictionary<int, EventHandler>();

    public void RegisterEvent(int eventId, EventHandler handler)
    {
        if (m_EventHandlers.ContainsKey(eventId))
        {
            m_EventHandlers[eventId] += handler;
        }
        else
        {
            m_EventHandlers.Add(eventId, handler);
        }
    }

    public void UnRegisterEvent(int eventId, EventHandler handler)
    {
        if (m_EventHandlers.ContainsKey(eventId))
        {
            m_EventHandlers[eventId] -= handler;
        }
    }

    public void TriggerEvent(int eventId, object sender)
    {
        if (m_EventHandlers.ContainsKey(eventId))
        {
            m_EventHandlers[eventId]?.Invoke(sender, EventArgs.Empty);
        }
    }

    public void TriggerEvent(int eventId, object sender, EventArgs args)
    {
        
        if (m_EventHandlers.ContainsKey(eventId))
        {
            m_EventHandlers[eventId]?.Invoke(sender, args);
        }
    }

    public void Clear()
    {
        m_EventHandlers.Clear();
    }
}